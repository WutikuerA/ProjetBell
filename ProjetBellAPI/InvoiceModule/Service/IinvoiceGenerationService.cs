using AssetModule.Models;
using InvoiceModule.Models;

namespace InvoiceModule.Service
{
    public interface IinvoiceGenerationService
    {
        Task<Invoice> GenerateInvoiceAsync (List<Asset> assetList);
        Task<Invoice> GenerateInvoiceFromDBAsync(); 
    }
}
