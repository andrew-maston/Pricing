using System.Collections.Generic;

namespace Pricing.Models.Dtos
{
    public class InvoiceRequestDto
    {
        public IReadOnlyCollection<OrderItem> Items { get; set; }
    }
}