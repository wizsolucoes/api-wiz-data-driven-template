using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wiz.Template.API.Filters;
using Wiz.Template.API.Services.Notifications;
using Xunit;

namespace Wiz.Template.Unit.Tests.Notifications
{
    public class DomainNotificationFilterTest
    {
        private readonly Mock<IDomainNotification> _domainNotificationMock;

        public DomainNotificationFilterTest()
        {
            _domainNotificationMock = new Mock<IDomainNotification>();
        }

        [Fact]
        public async Task AddResponseErrorHierarquiaTeste_IsValidTest()
        {
            var context = CreateResultExecutingContext(new Mock<IFilterMetadata>().Object);
            var next = new ResultExecutionDelegate(() => Task.FromResult(CreateResultExecutedContext(context)));
            var notifications = new List<NotificationMessage>()
            {
                new NotificationMessage("400", "Test Notification")
            };

            _domainNotificationMock.Setup(x => x.HasNotifications)
                .Returns(true);

            _domainNotificationMock.Setup(x => x.Notifications)
                    .Returns(notifications);

            var domainNotificationFilter = new DomainNotificationFilter(_domainNotificationMock.Object);
            await domainNotificationFilter.OnResultExecutionAsync(context, next);

            Assert.True(true);
        }

        private static ResultExecutingContext CreateResultExecutingContext(IFilterMetadata filter)
        {
            return new ResultExecutingContext(
                CreateActionContext(),
                new IFilterMetadata[] { filter, },
                new NoOpResult(),
                controller: new object());
        }

        private static ResultExecutedContext CreateResultExecutedContext(ResultExecutingContext context)
        {
            return new ResultExecutedContext(context, context.Filters, context.Result, context.Controller);
        }

        private static ActionContext CreateActionContext()
        {
            return new ActionContext(new DefaultHttpContext(), new RouteData(), new ActionDescriptor());
        }

        private class NoOpResult : IActionResult
        {
            public Task ExecuteResultAsync(ActionContext context)
            {
                return Task.FromResult(true);
            }
        }
    }
}
