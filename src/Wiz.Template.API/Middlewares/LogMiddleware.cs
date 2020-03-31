using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Wiz.Template.API.Services.Identity;

namespace Wiz.Template.API.Middlewares
{
    public class LogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly TelemetryClient _telemetry;

        public LogMiddleware(RequestDelegate next, TelemetryClient telemetry)
        {
            _next = next;
            _telemetry = telemetry;
        }

        public async Task Invoke(HttpContext context, IIdentityService identityService)
        {
            var method = context.Request.Method;

            _telemetry.TrackTrace(new TraceTelemetry(identityService.GetScope(), SeverityLevel.Information));

            if (HttpMethods.IsPost(method) || HttpMethods.IsPut(method) || HttpMethods.IsPatch(method))
            {
                var body = await FormatRequestBody(context.Request);

                _telemetry.TrackTrace(new TraceTelemetry(body, SeverityLevel.Information));
            }

            await _next(context);
        }

        private async Task<string> FormatRequestBody(HttpRequest request)
        {
            var body = string.Empty;

            request.EnableBuffering(bufferThreshold: 1024 * 45, bufferLimit: 1024 * 100);

            using (var reader = new StreamReader(request.Body, Encoding.UTF8, true, 1024, true))
            {
                body = await reader.ReadToEndAsync();
            }

            request.Body.Position = 0;

            return body;
        }
    }
}
