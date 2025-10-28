using System;
using AzureFunctionApp1.Data;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AzureFunctionApp1
{
    public class UpdateStatusToComplete
    {
        private readonly ILogger _logger;
        private readonly ApplicationDBContext _dBContext;

        public UpdateStatusToComplete(ILoggerFactory loggerFactory,ApplicationDBContext dBContext)
        {
            _logger = loggerFactory.CreateLogger<UpdateStatusToComplete>();
            _dBContext = dBContext;
        }

        [Function("UpdateStatusToComplete")]
        public void Run([TimerTrigger("0 */5 * * * *")] TimerInfo myTimer)
        {
            IEnumerable<SalesRequest> salesRequests = _dBContext.SalesRequests.Where(s => s.Status == "").ToList();
            foreach(var s in salesRequests)
            {
                s.Status = "Completed";
                _dBContext.SalesRequests.Update(s);
            }
            _dBContext.SaveChanges();
           
            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            
            if (myTimer.ScheduleStatus is not null)
            {
                _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
            }
        }
    }
}
