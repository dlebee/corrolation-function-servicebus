using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;

namespace FunctionApp
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

            //[{ "operation_Id":"aa31755a6b4afe41ac861a40da4a41e8","id":"043fa089064c1b48"}]

            // trying links.
            var links = activity.Links
                .Select(link => {
                    return new
                    {
                        operation_id = link.Context.TraceId.ToString(),
                        id = link.Context.SpanId.ToString()
                    };
                })
                .ToList();

            activity.AddTag("_MS.links", JsonSerializer.Serialize(links));
        }
    }
}
