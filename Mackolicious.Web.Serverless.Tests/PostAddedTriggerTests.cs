using Xunit;
using System.IO;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Mackolicious.Web.Serverless.Abstractions;
using Microsoft.Azure.Management.ResourceManager.Fluent.Authentication;

namespace Mackolicious.Web.Serverless.Tests
{
    public class PostAddedTriggerTests
    {
        [Fact]
        public void PostAddedTriggerRunSuccess()
        {
            var azureManagementAbstraction = new Mock<IAzureManagementAbstraction>();

            var log = NullLoggerFactory.Instance.CreateLogger("Null Logger");

            var content = System.Text.Encoding.UTF8.GetBytes("Hello");

            var postAddedTrigger = new PostAddedTrigger(azureManagementAbstraction.Object);

            using (var blobStream = new MemoryStream(content))
            {
                postAddedTrigger.Run(blobStream, "posts/{name}", log).Wait();
            }

            azureManagementAbstraction
                .Verify(a => a.GetCredentials(It.IsAny<string>(),
                                It.IsAny<string>(),
                                It.IsAny<string>()),
                            Times.Exactly(1));

            azureManagementAbstraction
                .Verify(a => a.GetWebApp(It.IsAny<AzureCredentials>(),
                                It.IsAny<string>(),
                                It.IsAny<string>()),
                            Times.Exactly(1));
        }
    }
}
