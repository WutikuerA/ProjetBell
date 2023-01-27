using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetBellAPI.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public DateTime IssueDate { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public Decimal TotalPrice { get; set; }

    }
}
