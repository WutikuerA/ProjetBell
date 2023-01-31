using AssetModule.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using ProjetBellAPI;
using System.Net.Http.Json;

namespace ProjectBellAPI.IntegrationTesting
{
    public class ProjectBellAPIIntegrationTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public ProjectBellAPIIntegrationTest(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetAssetWithId1_ShouldReturnAsset_Test()
        {
            // Arange
            var client = _factory.CreateDefaultClient();

            // Act
            var response = await client.GetAsync("/api/Asset/GetAssets");
            List<Asset>? assetList = response?.Content?.ReadFromJsonAsync<List<Asset>>()?.Result;

            //Assert
            // By default we have 3 data seeds when app is launched for the 1st time
            Assert.Equal(assetList.Count, 3);
            Assert.Equal(assetList[0].Name, "Service1");
            Assert.Equal(assetList[0].Price, 20);
            // ...
        }
    }
}
