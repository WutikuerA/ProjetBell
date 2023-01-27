using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetBellAPI.Filters
{
    public class InvoiceFilter
    {
        public int? Year { get; set; }
        public int? Month { get; set; }
        public decimal? PriceAbove { get; set; }
    }
}
