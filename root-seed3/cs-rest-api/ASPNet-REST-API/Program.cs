using ASPNet_REST_API;
using Auth0.AspNetCore.Authentication;
using CLIClassLibrary;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using SSoTme.Default.Lib.CLIHandler;
using System.Text;

var urls = new[] { "https://*:42123" };
var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls(urls);

var settingsSection = ConfigurationHelper.GetSection("auth0");
settingsSection = ConfigurationHelper.GetSection("auth0");
Auth0Settings.Instance = ConfigurationHelper.GetAuth0Settings();
builder.Services.Configure<Auth0Settings>(settingsSection);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});


var ClientId = Auth0Settings.Instance.ClientId;
var Domain = Auth0Settings.Instance.Domain;
var Audience = Auth0Settings.Instance.Audience;
var ClientSecret = Auth0Settings.Instance.ClientSecret;

// Add services to the container.
var authenticationBuilder = builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = Auth0Constants.AuthenticationScheme;
    options.DefaultForbidScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.Authority = $"https://{Domain}/";  // e.g. "https://your-app-name.auth0.com/"
    options.Audience = Audience;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = Domain, // Replace with your issuer URL
        ValidateAudience = true,
        ValidAudience = Audience, // Replace with your audience
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ClientSecret)),
        ClockSkew = TimeSpan.Zero // Optionally adjust clock skew tolerance
    };
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            // Check if the token is received
            var token = context.Request.Headers["Authorization"];
            Console.WriteLine($"Token received: {token}");
            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            // Token was validated
            Console.WriteLine("Token was validated.");
            return Task.CompletedTask;
        },
        OnAuthenticationFailed = context =>
        {
            // Token validation failed
            Console.WriteLine($"Authentication failed: {context.Exception}");
            return Task.CompletedTask;
        }
    };
});

authenticationBuilder.AddAuth0WebAppAuthentication(options =>
{
    options.Domain = Domain;
    options.ClientId = ClientId;
    options.Scope = "openid email profile";
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IClaimsTransformation, CustomClaimsTransformation>();

var app = builder.Build();

// Use Static Files (for serving static content like JavaScript, CSS, images, etc.)
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "app")),
    RequestPath = "/app"
});

// Middleware to handle SPA fallback: Redirects to index.html if no API route or static file is found
app.Use(async (context, next) =>
{
    await next(); // Process the initial request through the pipeline.

    // Check if the response is a 404 and the request does not appear to be for a file or API endpoint
    if (context.Response.StatusCode == 404 && !Path.HasExtension(context.Request.Path.Value) && !context.Request.Path.StartsWithSegments("/api"))
    {
        // Log original path (for debugging purposes)
        Console.WriteLine("Original Path: " + context.Request.Path);

        // Rewrite the path to point to the index.html file
        context.Request.Path = "/app/index.html";
        context.Response.StatusCode = 200; // Reset the status code to 200 to continue processing

        // Re-invoke the middleware pipeline with the new path
        await context.Response.SendFileAsync(Path.Combine(builder.Environment.ContentRootPath, "app", "index.html"));
    }
});

// CORS setup (keep as you have correctly set up)
app.UseCors(policy =>
{
    policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
});

// Routing for API controllers
app.UseRouting();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TicTacToeDemo");
        c.EnablePersistAuthorization();
        c.OAuthClientId(ClientId);
        c.OAuthRealm(Domain);
        c.OAuthAppName("Swagger UI");
        c.OAuthUsePkce();
        var parms = new Dictionary<string, string> { };
        parms.Add("audience", Audience);
        c.OAuthAdditionalQueryStringParams(parms);
    });
}

// Authentication and Authorization
app.UseAuthentication();
app.UseAuthorization();

// Map controller routes
app.MapControllers();

// Application run command
app.Run();
