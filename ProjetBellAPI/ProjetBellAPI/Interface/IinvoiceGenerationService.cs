using ProjetBellAPI.Models;

namespace ProjetBellAPI.Interface
{
    public interface IinvoiceGenerationService
    {
        Task<Invoice> GenerateInvoiceAsync (List<Asset> assetList);
        Task<Invoice> GenerateInvoiceFromDBAsync(); 
    }
}
