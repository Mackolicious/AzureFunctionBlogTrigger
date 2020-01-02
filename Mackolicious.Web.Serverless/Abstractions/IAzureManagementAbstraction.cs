using System.Threading.Tasks;
using Microsoft.Azure.Management.AppService.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Authentication;

namespace Mackolicious.Web.Serverless.Abstractions
{
    public interface IAzureManagementAbstraction
    {
        AzureCredentials GetCredentials(string clientId, string clientSecret, string tenantId);

        Task<IWebApp> GetWebApp(AzureCredentials credentials, string resourceGroupName, string webAppName);
    }
}