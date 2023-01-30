
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ProjectBellAPI.IntegrationTest
{
    public class IntegrationAPITest
    {
        private readonly HttpClient _testClient;

        public IntegrationAPITest(IConfiguration configuration)
        {
            var appFac = new WebApplicationFactory<Program>();
            _testClient = appFac.CreateDefaultClient();
        }

        [Fact]
        public async Task GetAssetWithId1_ShouldReturnAsset_Test()
        {
            // Arange
            /*var reponse = await _testClient.GetAsync("http://localhost/api/Assets/GetAssets/");

            // Act

            //Assert
            Assert.Equal("404", reponse.StatusCode.ToString());*/
            Assert.Equal(0, 1);
        }

    }
}