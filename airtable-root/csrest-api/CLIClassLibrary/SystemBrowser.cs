using EmbedIO;
using EmbedIO.Routing;
using EmbedIO.WebApi;
using IdentityModel.OidcClient.Browser;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace SSoTme.Default.Lib.CLIHandler
{
    public abstract partial class EAPICLIHandlerBase<T>
    {

        public class SystemBrowser : IBrowser
        {
            public int Port { get; set; }
            private readonly TaskCompletionSource<BrowserResult> _taskCompletionSource = new TaskCompletionSource<BrowserResult>();

            private WebServer _webServer;

            public string Url { get; set; }

            public SystemBrowser(int port)
            {
                Port = port;
            }

            public async Task NavigateAsync(Uri uri)
            {
                Process.Start("explorer.exe", uri.ToString()).WaitForExit();
            }

            public async Task CloseAsync()
            {
                if (_webServer != null)
                {
                    //await _webServer.StopAsync
                    _webServer.Dispose();
                    _webServer = null;
                }
            }

            private WebServer CreateWebServer(string url)
            {
                var server = new WebServer(o => o
                    .WithUrlPrefix(url)
                    .WithMode(HttpListenerMode.EmbedIO))
                    .WithWebApi("/callback", m => m.WithController(() => new CallbackController(_taskCompletionSource, this)));

                return server;
            }


            public Task<BrowserResult> InvokeAsync(BrowserOptions options, CancellationToken cancellationToken = default)
            {
                _webServer = CreateWebServer($"http://*:{Port}/");
                _webServer.RunAsync();

                // Open the URL in the browser
                Process.Start(new ProcessStartInfo { FileName = options.StartUrl, UseShellExecute = true });

                return _taskCompletionSource.Task;

            }
        }

        public class CallbackController : WebApiController
        {
            public CallbackController(SystemBrowser browser)
            {
                this.Browser = browser;
            }
            private TaskCompletionSource<BrowserResult> _tcs;

            public CallbackController(TaskCompletionSource<BrowserResult> tcs, SystemBrowser browser)
                : this(browser)
            {
                _tcs = tcs;
            }

            public SystemBrowser Browser { get; }

            [Route(HttpVerbs.Get, "/")]
            public async Task<string> GetCallback()
            {
                var url = $"{HttpContext.Request.Url}";

                Task.Factory.StartNew(() =>
                {
                    _tcs.SetResult(new BrowserResult
                    {
                        Response = $"{url}",
                        ResultType = BrowserResultType.Success
                    });
                }).Wait(1);

                return "You can now return to the application.";
            }
        }

    }
}