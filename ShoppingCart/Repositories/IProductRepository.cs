using ShoppingCart.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingCart
{
    public interface IProductRepository
    {
        public Task<IReadOnlyCollection<Product>> GetAllAsync();
    }
}