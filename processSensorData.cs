using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace LearnPrac.Function
{
    public static class processSensorData
    {
        [FunctionName("processSensorData")]
        public static async void Run(
            [EventHubTrigger("learn-prac-eventhub", Connection = "EventHubConnectionString")] TelemetryItem[] items,
            [CosmosDB(databaseName:"TelemetryDb",collectionName:"TelemetryInfo",ConnectionStringSetting="CosmosDBConnectionString")] IAsyncCollector<TelemetryItem> document,
            ILogger log)
        {
            var exceptions = new List<Exception>();

            foreach (TelemetryItem item in items)
            {
                try
                {
                    string messageBody = item.toString();

                    // Replace these two lines with your processing logic.
                    log.LogInformation($"C# Event Hub trigger function processed a message: {messageBody}");
                    if(item.getPressure() > 30){
                        item.setNormalPressure(false);
                    }else{
                        item.setNormalPressure(true);
                    }

                    if(item.getTemperature()<40){
                        item.setTemperatureStatus(status.COOL);
                    }else if(item.getTemperature()>90){
                        item.setTemperatureStatus(status.HOT);
                    }else{
                        item.setTemperatureStatus(status.WARM);
                    }

                    await document.AddAsync(item);
                }
                catch (Exception e)
                {
                    // We need to keep processing the rest of the batch - capture this exception and continue.
                    // Also, consider capturing details of the message that failed processing so it can be processed again later.
                    exceptions.Add(e);
                }
            }

            // Once processing of the batch is complete, if any messages in the batch failed processing throw an exception so that there is a record of the failure.

            if (exceptions.Count > 1)
                throw new AggregateException(exceptions);

            if (exceptions.Count == 1)
                throw exceptions.Single();
        }
    }
}
