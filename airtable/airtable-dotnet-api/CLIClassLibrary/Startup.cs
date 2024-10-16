using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using SSoTme.Default.Lib.CLIHandler;

namespace ConsoleApp1
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseFileServer();

            var provider = new FileExtensionContentTypeProvider();
            // Add new mappings
            provider.Mappings[".smql"] = "application/xml";
            provider.Mappings[".odxml"] = "application/xml";
            provider.Mappings[".txt"] = "application/text";


            PhysicalFileProvider fileProvider = new PhysicalFileProvider(EAPICLIHandlerBase.AssemblyDirectory);
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = fileProvider,
                ContentTypeProvider = provider,
                RequestPath = new PathString("")
            });

            DefaultFilesOptions options = new DefaultFilesOptions();
            options.FileProvider = fileProvider;
            options.DefaultFileNames.Clear();
            options.DefaultFileNames.Add("Index.html");
            options.DefaultFileNames.Add("index.html");
            options.DefaultFileNames.Add("ReadMe.html");
            options.DefaultFileNames.Add("readme.html");
            app.UseDefaultFiles(options);

            //app.UseDirectoryBrowser();

            app.UseCors("MyPolicy");
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
        }
    }
}