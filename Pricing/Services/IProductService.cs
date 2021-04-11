using Pricing.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pricing.Services
{
    /// <summary>
    /// Used to retrieve products
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Retrieves products
        /// </summary>
        /// <returns>IReadOnlyCollection of Products</returns>
        public Task<IReadOnlyCollection<Product>> GetAll();
    };
}