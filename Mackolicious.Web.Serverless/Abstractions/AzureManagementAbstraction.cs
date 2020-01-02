using System.Threading.Tasks;
using Microsoft.Azure.Management.AppService.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Authentication;

namespace Mackolicious.Web.Serverless.Abstractions
{
    public class AzureManagementAbstraction : IAzureManagementAbstraction
    {
        public AzureCredentials GetCredentials(string clientId, string clientSecret, string tenantId)
        {
            return SdkContext.AzureCredentialsFactory
                .FromServicePrincipal(clientId,
                clientSecret,
                tenantId,
                AzureEnvironment.AzureGlobalCloud);
        }

        public async Task<IWebApp> GetWebApp(AzureCredentials credentials, string resourceGroupName, string webAppName)
        {
            var azure = await Microsoft.Azure.Management.Fluent.Azure
                .Configure()
                .Authenticate(credentials)
                .WithDefaultSubscriptionAsync();

           return await azure.AppServices.WebApps.GetByResourceGroupAsync(resourceGroupName, webAppName);
        }
    }
}