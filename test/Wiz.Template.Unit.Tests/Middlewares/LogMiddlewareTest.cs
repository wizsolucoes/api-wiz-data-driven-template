using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Threading.Tasks;
using Wiz.Template.API.Middlewares;
using Wiz.Template.API.Services.Identity;
using Wiz.Template.Unit.Tests.Mocks;
using Xunit;

namespace Wiz.Template.Unit.Tests.Middlewares
{
    public class LogMiddlewareTest
    {
        private readonly Mock<IIdentityService> _identityServiceMock;

        public LogMiddlewareTest()
        {
            _identityServiceMock = new Mock<IIdentityService>();
        }

        [Fact]
        public async Task InvokeLogHandler_Test()
        {
            var fakeChannel = new TelemetryChannelMock();
            var config = new TelemetryConfiguration
            {
                TelemetryChannel = fakeChannel,
                InstrumentationKey = string.Empty,
            };
            var client = new TelemetryClient(config);

            var httpContext = new DefaultHttpContext().Request.HttpContext;
            httpContext.Request.Method = HttpMethods.Post;

            var logMiddleware = new LogMiddleware(async (innerHttpContext) =>
            {
                await innerHttpContext.Response.WriteAsync("Response body mock");
            }, client);

            await logMiddleware.Invoke(httpContext, _identityServiceMock.Object);

            Assert.NotNull(logMiddleware);
        }
    }
}
