
namespace AssetModule.Filters
{
    public class AssetFilter
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal PriceMoreThan { get; set; }
        public decimal PriceLessThan { get; set; }
        public DateTime? AfterDate { get; set; }
        public DateTime? BeforeDate { get; set; }
    }
}
