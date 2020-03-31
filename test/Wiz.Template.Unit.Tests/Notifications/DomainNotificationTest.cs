using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using System.Collections.Generic;
using Wiz.Template.API.Services.Notifications;
using Wiz.Template.Unit.Tests.Mocks;
using Xunit;

namespace Wiz.Template.Unit.Tests.Notifications
{
    public class DomainNotificationTest
    {
        private readonly TelemetryClient _telemetryClient;

        public DomainNotificationTest()
        {
            var config = new TelemetryConfiguration
            {
                TelemetryChannel = new TelemetryChannelMock(),
                InstrumentationKey = string.Empty,
            };

            _telemetryClient = new TelemetryClient(config);
        }

        [Fact]
        public void AddNotification_IsValidTest()
        {
            var notification = new DomainNotification(_telemetryClient);
            notification.AddNotification("400", "Test Notification", sendTelemetry: true);

            Assert.True(notification.HasNotifications);
        }

        [Fact]
        public void AddNotificationMessage_IsValidTest()
        {
            var notification = new DomainNotification(_telemetryClient);
            var notificationMessage = new List<NotificationMessage>
            {
                new NotificationMessage("400", "Test Notification")
            };

            notification.AddNotifications(notificationMessage, sendTelemetry: true);

            Assert.True(notification.HasNotifications);
        }

    }
}
