using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

[assembly: FunctionsStartup(typeof(FunctionApp.Startup))]
namespace FunctionApp
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            ActivitySource.AddActivityListener(new ActivityListener()
            {
                Sample = (ref ActivityCreationOptions<ActivityContext> _) =>
                {
                    return ActivitySamplingResult.AllData;
                },
                SampleUsingParentId = (ref ActivityCreationOptions<string> _) => ActivitySamplingResult.AllData,
                ShouldListenTo = s =>
                {
                    return true;
                }
            });

            builder.Services.AddSingleton<ITelemetryInitializer, ActivityTagsTelemetryInitializer>();
        }
    }
}
