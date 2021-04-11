using Pricing.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pricing.Repositories
{
    public interface IProductRepository
    {
        public Task<IReadOnlyCollection<Product>> GetAllAsync();
    }
}