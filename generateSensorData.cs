using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace LearnPrac.Function
{
    public static class generateSensorData
    {
        [FunctionName("generateSensorData")]
        [return: EventHub("learn-prac-eventhub", Connection = "EventHubConnectionString")]
        public static TelemetryItem Run([TimerTrigger("*/10 * * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            double temperature = new Random().NextDouble() * 100;
            double pressure = new Random().NextDouble()*50;
            return new TelemetryItem(temperature, pressure);
        }
    }
}
