using System.Collections.Generic;
using System.Linq;

namespace Pricing.Models
{
    public class Invoice
    {
        public IEnumerable<OrderRow> OrderRows { get; set; }

        public decimal SubTotal => OrderRows.Any(t => t != null) ? OrderRows.Sum(t => t.SubTotal) : 0;

        public decimal TotalWithSavings => OrderRows.Any(t => t != null) ? OrderRows.Sum(t => t.SubTotalWithSaving) : 0;

        public decimal TotalSaving => OrderRows.Any(t => t != null) ? OrderRows.Sum(t => t.Saving) : 0;

        public InvoiceStatus Status { get; set; }
    }
}