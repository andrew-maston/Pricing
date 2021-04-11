namespace Pricing.Models
{
    public class OrderRow
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal SubTotal { get; set; }
        public decimal SubTotalWithSaving => SubTotal - Saving;
        public decimal Saving { get; set; }
    }
}