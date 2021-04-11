using Pricing.Models;
using Pricing.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pricing.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IReadOnlyCollection<Product>> GetAll()
        {
            return await _productRepository.GetAllAsync();
        }
    }
}