using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Wiz.Template.API.Services.Notifications
{
    public class DomainNotification : IDomainNotification
    {
        private readonly List<NotificationMessage> _notifications;
        private readonly TelemetryClient _telemetry;


        public DomainNotification(TelemetryClient telemetry)
        {
            _notifications = new List<NotificationMessage>();
            _telemetry = telemetry;
        }

        public IReadOnlyCollection<NotificationMessage> Notifications => _notifications;

        public bool HasNotifications => _notifications.Any();

        public void AddNotification(string key, string message, bool sendTelemetry = false)
        {
            var notification = new NotificationMessage(key, message);

            if (sendTelemetry)
                SendTelemetry(notification);

            _notifications.Add(notification);
        }

        public void AddNotifications(IEnumerable<NotificationMessage> notifications, bool sendTelemetry = false)
        {
            if (sendTelemetry)
                SendTelemetry(notifications);

            _notifications.AddRange(notifications);
        }

        private void SendTelemetry(NotificationMessage notification)
        {
            _telemetry.TrackTrace(new TraceTelemetry(JsonConvert.SerializeObject(notification), SeverityLevel.Information));
        }

        private void SendTelemetry(IEnumerable<NotificationMessage> notifications)
        {
            _telemetry.TrackTrace(new TraceTelemetry(JsonConvert.SerializeObject(notifications), SeverityLevel.Information));
        }
    }
}
