using Microsoft.ApplicationInsights.Channel;
using System;
using System.Collections.Concurrent;

namespace Wiz.Template.Unit.Tests.Mocks
{
    public class TelemetryChannelMock : ITelemetryChannel
    {
        public ConcurrentBag<ITelemetry> SentTelemtries =
                               new ConcurrentBag<ITelemetry>();
        public bool IsFlushed { get; private set; }
        public bool? DeveloperMode { get; set; }
        public string EndpointAddress { get; set; }
        private bool disposedValue = false;

        public void Send(ITelemetry item)
        {
            SentTelemtries.Add(item);
        }
        public void Flush()
        {
            IsFlushed = true;
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
                disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
