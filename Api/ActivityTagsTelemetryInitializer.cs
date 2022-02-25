using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using System.Diagnostics;

namespace Api
{
    public class ActivityTagsTelemetryInitializer : ITelemetryInitializer
    {
        public void Initialize(ITelemetry telemetry)
        {
            var activity = Activity.Current;
            var requestTelemetry = telemetry as ISupportProperties;

            if (requestTelemetry == null || activity == null) return;

            foreach (var tag in activity.Tags)
            {
                requestTelemetry.Properties[tag.Key] = tag.Value;
            }
        }
    }
}
