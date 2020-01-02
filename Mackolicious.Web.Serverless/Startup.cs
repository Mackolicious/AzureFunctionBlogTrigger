using Mackolicious.Web.Serverless.Abstractions;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Mackolicious.Web.Serverless.Startup))]

namespace Mackolicious.Web.Serverless
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<IAzureManagementAbstraction, AzureManagementAbstraction>();
        }
    }
}