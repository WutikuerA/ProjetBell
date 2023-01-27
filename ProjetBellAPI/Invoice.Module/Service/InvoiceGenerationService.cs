﻿
using AssetModule.DataService;
using AssetModule.Models;
using InvoiceModule.DataService;
using InvoiceModule.Models;
using InvoiceModule.Service;

namespace InvoiceModule.Services
{
    public class InvoiceGenerationService : IinvoiceGenerationService
    {
        private AssetDataContext _assetDBContext;

        public InvoiceGenerationService( ){ }

        public InvoiceGenerationService(AssetDataContext assetDBContext)
        {
            _assetDBContext = assetDBContext;
        }

        public async Task<Invoice> GenerateInvoiceAsync(List<Asset> assetList)
        {
            DateTime today = DateTime.Now;
            Invoice invoice = new Invoice()
            {
                // This process would be better with a SQL query to avoid unnecessary traffic between DB and backend
                // as the function below, this function is created for unit test presentation only
                IssueDate = today,
                Month = DateTime.Now.Month,
                Year = DateTime.Now.Year,
                TotalPrice = assetList.Where(a =>
                    (a.ValidFrom == null && a.ValidTo == null) ||
                    (a.ValidFrom == null && a.ValidTo != null && a.ValidTo > today) ||
                    (a.ValidFrom != null && a.ValidTo == null && a.ValidFrom < today) ||
                    (a.ValidFrom != null && a.ValidTo != null && a.ValidFrom < today && today < a.ValidTo)
                ).Sum(a => a.Price)
            };

            return invoice;
        }

        public async Task<Invoice> GenerateInvoiceFromDBAsync()
        {
            DateTime today = DateTime.Now;

            decimal price = (from assets in _assetDBContext.Asset
                            where   (assets.ValidFrom == null && assets.ValidTo == null) ||
                                    (assets.ValidFrom == null && assets.ValidTo != null && assets.ValidTo > today) ||
                                    (assets.ValidFrom != null && assets.ValidTo == null && assets.ValidFrom < today) ||
                                    (assets.ValidFrom != null && assets.ValidTo != null && assets.ValidFrom < today && today < assets.ValidTo)
                            select assets).Sum(a => a.Price);

            Invoice invoice = new Invoice()
            {
                // This process would be better with a stocked procedure on SQL side
                // to avoid unnecessary traffic between DB and backend
                IssueDate = today,
                Month = DateTime.Now.Month,
                Year = DateTime.Now.Year,
                TotalPrice = price
            };

            return invoice;
        }
    }
}
