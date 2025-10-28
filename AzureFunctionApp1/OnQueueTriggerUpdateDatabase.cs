using Azure.Storage.Queues.Models;
using AzureFunctionApp1.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;

namespace AzureFunctionApp1
{
    public class OnQueueTriggerUpdateDatabase
    {
        private readonly ILogger<OnQueueTriggerUpdateDatabase> _logger;
        private readonly ApplicationDBContext _dbContext;
        public OnQueueTriggerUpdateDatabase(ILogger<OnQueueTriggerUpdateDatabase> logger,ApplicationDBContext dBContext)
        {
            _logger = logger;
            _dbContext = dBContext;
        }

        [Function(nameof(OnQueueTriggerUpdateDatabase))]
        //Connection =AzureWebJobsStorage from local.settings.json
        public void Run([QueueTrigger("SalesRequestInbound", Connection = "AzureWebJobsStorage")] QueueMessage message)
        {
            string messageBody= message.Body.ToString();
            var salesRequest= JsonConvert.DeserializeObject<SalesRequest>(messageBody);
            if (salesRequest != null)
            {
                _dbContext.SalesRequests.Add(salesRequest);
                _dbContext.SaveChanges();
            }
            else
            {
                _logger.LogInformation($"Failed to deserialize the object: {message.MessageText}");

            }
           
        }

    }
}
