using AzureFunctionApp1.Data;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

//var builder = FunctionsApplication.CreateBuilder(args);

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        string connectionString = Environment.GetEnvironmentVariable("AzureSQLDatabase");
        services.AddDbContext<ApplicationDBContext>(options =>
            options.UseSqlServer(connectionString));
    });


host.Build().Run();
