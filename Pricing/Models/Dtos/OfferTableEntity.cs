using Microsoft.Azure.Cosmos.Table;

namespace Pricing.Models.Dtos
{
    public class OfferTableEntity : TableEntity
    {
        public string QualifyingProductName { get; set; }
        public int QuantityRequired { get; set; }
        public string ModifiedProductName { get; set; }
        public decimal PriceModifier { get; set; }
    }
}
