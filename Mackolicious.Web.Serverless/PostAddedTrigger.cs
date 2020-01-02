using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Mackolicious.Web.Serverless.Abstractions;

namespace Mackolicious.Web.Serverless
{
    public class PostAddedTrigger
    {
        private readonly IAzureManagementAbstraction _azureManagementAbstraction;
        public PostAddedTrigger(IAzureManagementAbstraction azureManagementAbstraction)
        {
            _azureManagementAbstraction = azureManagementAbstraction;
        }

        //Triggered after new blob is added or modified as a result of adding a new blog post
        [FunctionName("PostAddedTrigger")]
        public async Task Run([BlobTrigger("posts/{name}", Connection = "blob_STORAGE")]Stream myBlob, string name, ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");

            var clientId = GetEnvironmentVariable("clientId");
            var clientSecret = GetEnvironmentVariable("clientSecret");
            var tenantId = GetEnvironmentVariable("tenantId");
            var webAppName = GetEnvironmentVariable("webAppName");
            var webAppResourceGroupName = GetEnvironmentVariable("webAppResourceGroupName");

            var credentials = _azureManagementAbstraction.GetCredentials(clientId, clientSecret, tenantId);

            var webApp = await _azureManagementAbstraction.GetWebApp(credentials, webAppResourceGroupName, webAppName);

            if (webApp == null)
            {
                log.LogError($"Web App {webAppName} not found");
            }
            else
            {
                webApp.Restart();
                
                log.LogInformation($"Web App {webAppName} restarted");
            }
        }

        public static string GetEnvironmentVariable(string name)
        {
            return System.Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process);
        }
    }
}