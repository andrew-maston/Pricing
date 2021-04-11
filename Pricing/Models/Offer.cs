namespace Pricing.Models
{
    public class Offer
    {
        public string QualifyingProductName { get; set; }
        public int QuantityRequired { get; set; }
        public OfferModifier Discount { get; set; }
    }
}