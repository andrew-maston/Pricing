using Microsoft.Azure.Cosmos.Table;
using ShoppingCart.Models;
using ShoppingCart.Models.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart
{
    public class ProductRepository : IProductRepository
    {
        private const string TableName = "Product";
        private readonly CloudTable CloudTable; 

        public ProductRepository(CloudStorageAccount cloudStorageAccount)
        {
            var client = cloudStorageAccount.CreateCloudTableClient();
            CloudTable = client.GetTableReference(TableName);
            CloudTable.CreateIfNotExists();
        }

        public async Task<IReadOnlyCollection<Product>> GetAllAsync()
        {
            var results = await CloudTable.GetAllAsync<ProductTableEntity>();

            return (results != null ? results.Select(r => new Product
            {
                Name = r.Name,
                Price = r.Price
            }) : new List<Product>()).ToArray();
        }
    }
}
