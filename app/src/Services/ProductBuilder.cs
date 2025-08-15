
using System;
using System.Collections.Generic;
using System.Globalization;
namespace SalesTaxNS
{
    public class ProductBuilder
    {
        private readonly string[] exemptKeywords = { "book", "chocolate", "pill" };

        public Product BuildProductFromInput(string line)
        {
            var parts = line.Split(" at ");
            var qtyAndName = parts[0].Split(' ', 2);

            int quantity = int.Parse(qtyAndName[0]);
            string name = qtyAndName[1];

            decimal priceExclusiveTax = decimal.Parse(parts[1], CultureInfo.InvariantCulture);
            decimal tax = TaxCalculator.CalculateTax(priceExclusiveTax, IsExempt(name), IsImported(name));
            decimal finalPrice = priceExclusiveTax + tax;

            return new Product
            {
                InputLine = line,
                Quantity = quantity,
                Name = name,
                PriceExclusiveTax = priceExclusiveTax,
                Tax = tax,
                FinalPrice = finalPrice
                
            };
        }

        private bool IsExempt(string name)
        {
            foreach (var keyword in exemptKeywords)
            {
                if (name.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }

        private bool IsImported(string name)
        {
            return name.Contains("imported", StringComparison.OrdinalIgnoreCase);
        }
    }
}