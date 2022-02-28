using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace FunctionApp
{
 
    public class MessageReceiver
    {
        [FunctionName("MessageReceiver")]
        public static void Run([ServiceBusTrigger("message-queue", Connection = "ServiceBusConnection")] ServiceBusReceivedMessage message, ILogger log)
        {
            var diagnosticParentId = message.ApplicationProperties["Diagnostic-Id"] as string;
            var actualMessage = Encoding.UTF8.GetString(message.Body);
            var activity = Activity.Current;
            activity.SetParentId(diagnosticParentId);
            activity.AddTag("FromServiceBus", "1");
            activity.AddTag("FromUserPropertiesDiagnosticId", diagnosticParentId);
            activity.AddTag("Message", actualMessage);
            log.LogInformation("Message coming from message queue is {message}", actualMessage);
        }
    }
}
