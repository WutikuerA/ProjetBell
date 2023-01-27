
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetBellAPI.DataService;
using ProjetBellAPI.Filters;
using ProjetBellAPI.Interface;
using ProjetBellAPI.Models;
using ProjetBellAPI.Services;

namespace ProjetBellAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly DataContext _dBContext;
        private readonly IinvoiceGenerationService _invoiceGenerationService;
        public InvoiceController(DataContext dBContext, IinvoiceGenerationService invoiceGenerationService)
        {
            _dBContext = dBContext;
            _invoiceGenerationService = invoiceGenerationService;
        }

        [HttpGet("GetInvoices")]
        public List<Invoice> GetInvoices([FromQuery] InvoiceFilter filter)
        {
            var invoiceList = from Invoice in _dBContext.Invoice orderby Invoice.IssueDate descending select Invoice;

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
            var assetList = from assets in _dBContext.Assets select assets;

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
            var lastChangedAsset = (from assets in _dBContext.Assets
                            orderby assets.ModificationDate descending
                            select assets).FirstOrDefault();

            //Get the last invoice generation date
            var lastIssuedInvoice = (from invoice in _dBContext.Invoice
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
                _dBContext.Invoice.Add(invoice);
                _dBContext.SaveChanges();
            }
            catch (Exception ex) 
            { 
            }
        }


    }
}
