using AssetModule.Models;
using AutoFixture;
using InvoiceModule.Services;

namespace ProjectBellAPI.UnitTest
{
    public class InvoiceGenerationServiceTest
    {
        Fixture _fixture = new Fixture();
        const int _price = 10;

        public InvoiceGenerationServiceTest()
        {
        }

        [Fact]
        public async Task When_NoAssetIsCurrentlyValid_TotalPriceIsZero()
        {
            // Arrange
            var invoiceGenerationService = new InvoiceGenerationService();
            Asset asset1 = _fixture.Build<Asset>().With(a => a.ValidTo, DateTime.Today.AddDays(-5)).Create();
            Asset asset2 = _fixture.Build<Asset>().With(a => a.ValidFrom, DateTime.Today.AddDays(5)).Create();

            List<Asset> assetList = new List<Asset>() { asset1, asset2};

            //Act
            var invoice = await invoiceGenerationService.GenerateInvoiceAsync(assetList);

            //Assert
            Assert.Equal(0, invoice.TotalPrice);
            //...

        }

        [Fact]
        public async Task When_2AssetIsValidAndOtherAssetIsNot_TotalPriceEquals2AssetPriceSum()
        {
            // Arrange
            var invoiceGenerationService = new InvoiceGenerationService();
            Asset asset1 = _fixture.Build<Asset>()
                .With(a => a.ValidFrom,DateTime.Today.AddDays(-5))
                .With(a => a.ValidTo, DateTime.Today.AddDays(5))
                .With(a => a.Price, _price)
                .Create();

            Asset asset2 = _fixture.Build<Asset>()
                .With(a => a.ValidTo, DateTime.Today.AddDays(-5)).Create();

            List<Asset> assetList = new List<Asset>() { asset1, asset2 };

            //Act
            var invoice = await invoiceGenerationService.GenerateInvoiceAsync(assetList);



            //Assert
            Assert.Equal(_price, invoice.TotalPrice);
            //...
        }


    }
}