namespace Pricing.Controllers
{
    public class RowCost
    {
        public double SubTotal { get; set; }
        public double SubTotalWithSavings { get; set; }
        public double Savings => SubTotal - SubTotalWithSavings;
    }
}