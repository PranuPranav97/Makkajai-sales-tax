namespace SalesTaxNS
{
    
    public class TaxCalculator
    {
        //Making the Utility stateless and pure utility methods.
        public static decimal CalculateTax(decimal priceExclTax, bool isExempt, bool imported)
        {
            decimal taxRate = 0;

            if (!isExempt)
                taxRate += 0.10m;

            if (imported)
                taxRate += 0.05m;

            decimal tax = priceExclTax * taxRate;
            return RoundUpToNearest005(tax);
        }

        private static decimal RoundUpToNearest005(decimal value)
        {
            return Math.Ceiling(value * 20) / 20;
        }
    }
}
