using Microsoft.Azure.Cosmos.Table;
using ShoppingCart.Models;
using ShoppingCart.Models.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart
{
    public class OfferRepository : IOfferRepository
    {
        private const string TableName = "Offer";
        private readonly CloudTable CloudTable; 

        public OfferRepository(CloudStorageAccount cloudStorageAccount)
        {
            var client = cloudStorageAccount.CreateCloudTableClient();
            CloudTable = client.GetTableReference(TableName);
            CloudTable.CreateIfNotExists();
        }

        public async Task<IReadOnlyCollection<Offer>> GetAllAsync()
        {
            var results = await CloudTable.GetAllAsync<OfferTableEntity>();

            return (results != null ? results.Select(r => new Offer
            {
                QualifyingProductName = r.QualifyingProductName,
                QuantityRequired = r.QuantityRequired,
                Modifier = new OfferModifier
                {
                    ModifiedProductName = r.ModifiedProductName,
                    PriceModifier = r.PriceModifier,
                }
            }) : new List<Offer>()).ToArray();
        }
    }
}
