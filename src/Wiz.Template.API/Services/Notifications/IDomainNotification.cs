using System.Collections.Generic;

namespace Wiz.Template.API.Services.Notifications
{
    public interface IDomainNotification
    {
        bool HasNotifications { get; }
        IReadOnlyCollection<NotificationMessage> Notifications { get; }
        void AddNotification(string key, string message, bool sendTelemetry = false);
        void AddNotifications(IEnumerable<NotificationMessage> notifications, bool sendTelemetry = false);
    }
}
