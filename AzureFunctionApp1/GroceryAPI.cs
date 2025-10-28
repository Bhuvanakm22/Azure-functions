using AzureFunctionApp1.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AzureFunctionApp1
{
    public class GroceryAPI
    {
        private readonly ILogger<GroceryAPI> _logger;
        private readonly ApplicationDBContext _dbContext;
        public GroceryAPI(ILogger<GroceryAPI> logger, ApplicationDBContext dBContext)
        {
            _logger = logger;
            _dbContext = dBContext;
        }

        [Function("GroceryAPI")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation(" HTTP route function processed a request.");
            return new OkObjectResult("Welcome to Azure Functions!");
        }
        [Function("GetGrocery")]
        public IActionResult GetGrocery([HttpTrigger(AuthorizationLevel.Function, "get", Route = "SalesList")] HttpRequest req)
        {
            var data = _dbContext.SalesRequests.ToList();
            _logger.LogInformation(" HTTP route function processed a request.");
            return new OkObjectResult(data);
        }
    }
}
