using Microsoft.Azure.Cosmos.Table;

namespace ShoppingCart.Models.Dtos
{
    public class ProductTableEntity : TableEntity
    {
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
