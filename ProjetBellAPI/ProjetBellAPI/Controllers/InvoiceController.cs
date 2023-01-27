
using AssetModule.DataService;
using InvoiceModule.DataService;
using InvoiceModule.Filters;
using InvoiceModule.Models;
using InvoiceModule.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ProjetBellAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly AssetDataContext _assetDBContext;
        private readonly InvoiceDataContext _invoiceDBContext;
        private readonly IinvoiceGenerationService _invoiceGenerationService;
        public InvoiceController(AssetDataContext assetDBContext, InvoiceDataContext invoiceDataContext, IinvoiceGenerationService invoiceGenerationService)
        {
            _assetDBContext = assetDBContext;
            _invoiceDBContext = invoiceDataContext;
            _invoiceGenerationService = invoiceGenerationService;
        }

        [HttpGet("GetInvoices")]
        public List<Invoice> GetInvoices([FromQuery] InvoiceFilter filter)
        {
            var invoiceList = from Invoice in _invoiceDBContext.Invoice orderby Invoice.IssueDate descending select Invoice;

            // Apply filter search on name
            List<Invoice> filteredList = new List<Invoice>();
            if (filter.PriceAbove != null)
            {
                filteredList = invoiceList.Where(a => a.TotalPrice > filter.PriceAbove).ToList();
                return filteredList;
            }


            return invoiceList.ToList();
        }

        [HttpGet("GenerateInvoice")]
        public async Task<Invoice> GenerateInvoice()
        {
            // Get all assets
            var assetList = from assets in _assetDBContext.Asset select assets;

            // Generate invoice info
            Invoice invoice = await _invoiceGenerationService.GenerateInvoiceFromDBAsync();

            // Save the invoice back to DB
            saveNewInvoice(invoice);

            return invoice;
        }

        [HttpGet("IsAssetChanged")]
        public bool IsAssetChanged()
        {
            // Get the last asset modification date
            var lastChangedAsset = (from assets in _assetDBContext.Asset
                            orderby assets.ModificationDate descending
                            select assets).FirstOrDefault();

            //Get the last invoice generation date
            var lastIssuedInvoice = (from invoice in _invoiceDBContext.Invoice
                                    orderby invoice.IssueDate descending
                                    select invoice).FirstOrDefault();

            if (lastIssuedInvoice == null)
                return true; // no invoice generated yet

            if (lastChangedAsset == null)
                return false;

            bool result = lastChangedAsset.ModificationDate > lastIssuedInvoice.IssueDate;

            return result;
        }

        private void saveNewInvoice(Invoice invoice)
        {
            try
            {
                _invoiceDBContext.Invoice.Add(invoice);
                _invoiceDBContext.SaveChanges();
            }
            catch (Exception ex) 
            { 
            }
        }


    }
}
