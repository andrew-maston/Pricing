namespace ShoppingCart
{
    public class Offer
    {
        public string QualifyingProductName { get; set; }
        public int QuantityRequired { get; set; }
        public OfferModifier Modifier { get; set; }
    }
}