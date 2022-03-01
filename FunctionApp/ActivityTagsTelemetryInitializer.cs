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

            // trying links.
            if (activity.Links.Any())
            {
                var links = activity.Links
                    .Select(link =>
                    {
                        return new
                        {
                            operation_id = link.Context.TraceId.ToString(),
                            id = link.Context.SpanId.ToString()
                        };
                    })
                    .ToList();

                requestTelemetry.Properties["_MS.links"] = JsonSerializer.Serialize(links);
            }
        }
    }
}
