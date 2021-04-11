using Pricing.Models;
using Pricing.Models.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pricing.Services
{
    /// <summary>
    /// Used to calculate invoices
    /// </summary>
    public interface IInvoiceService
    {
        /// <summary>
        /// Calculates an invoice given a collection of order items
        /// </summary>
        /// <param name="orderItems"></param>
        /// <returns>Calculated Invoice</returns>
        public Task<Invoice> Calculate(IReadOnlyCollection<OrderItem> orderItems);
    }
}