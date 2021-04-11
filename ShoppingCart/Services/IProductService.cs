using ShoppingCart.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingCart
{
    public interface IProductService
    {
        public Task<IReadOnlyCollection<Product>> GetAll();
    };
}