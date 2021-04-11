using Pricing.Models;
using Pricing.Models.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace Pricing.Extensions
{
    /// <summary>
    /// Extensions for a collection of OrderItem
    /// </summary>
    public static class OrderItemsExtensions
    {
        /// <summary>
        /// Adapts OrderItems to OrderRows
        /// </summary>
        /// <param name="orderItems"></param>
        /// <param name="products"></param>
        /// <returns></returns>
        public static List<OrderRow> GetOrderRows(this IReadOnlyCollection<OrderItem> orderItems, IEnumerable<Product> products)
        {
            return orderItems.Select(item =>
             {
                 //We'd probably want a guid here to lookup the product, we wouldn't really use the name as a key
                 var product = products.FirstOrDefault(p => p.Name == item.ProductName);
                 var row = new OrderRow
                 {
                     //uses a product without price for error handling - we'd probably want a result status or something else more verbose ideally
                     Product = product ?? new Product { Name = item.ProductName},
                     Quantity = item.Quantity,
                     SubTotal = product != null ? product.Price * item.Quantity : 0
                 };

                 return row;

             }).ToList();
        }
    }
}
