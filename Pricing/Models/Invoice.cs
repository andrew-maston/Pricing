using System.Collections.Generic;
using System.Linq;

namespace Pricing.Models
{
    public class Invoice
    {
        public IEnumerable<OrderRow> OrderRows { get; set; }

        public string SubTotal => (OrderRows.Any(t => t != null) ? OrderRows.Sum(t => t.SubTotal) : 0).ToString("C");

        public string TotalWithSavings => (OrderRows.Any(t => t != null) ? OrderRows.Sum(t => t.SubTotalWithSaving) : 0).ToString("C");

        public string TotalSaving => (OrderRows.Any(t => t != null) ? OrderRows.Sum(t => t.Saving) : 0).ToString("C");

        public InvoiceStatus Status { get; set; }
    }
}