using System;
using System.Collections.Generic;
using System.Globalization;

namespace SalesTaxNS
{
    public class Program
    {
        private static readonly List<Product> products = new List<Product>();

        public static void Main(string[] args)
        {
            while (true)
            {
                string input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                    break;

                products.Add(ProductBuilder.BuildProductFromInput(input));
            }

            ReceiptPrinter.PrintReceipt(products);
        }
    }

    public static class ProductBuilder
    {
        private static readonly string[] exemptKeywords = { "book", "chocolate", "pill" };

        public static Product BuildProductFromInput(string line)
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

        private static bool IsExempt(string name)
        {
            foreach (var keyword in exemptKeywords)
            {
                if (name.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }

        private static bool IsImported(string name)
        {
            return name.Contains("imported", StringComparison.OrdinalIgnoreCase);
        }
    }

    public static class TaxCalculator
    {
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

    public static class ReceiptPrinter
    {
        public static void PrintReceipt(IEnumerable<Product> products)
        {
            decimal totalAmount = 0;
            decimal totalTax = 0;

            foreach (var p in products)
            {
                Console.WriteLine($"{p.Quantity} {p.Name}: {p.FinalPrice:F2}");
                totalTax += p.Tax;
                totalAmount += p.FinalPrice;
            }

            Console.WriteLine($"Sales Taxes: {totalTax:F2}");
            Console.WriteLine($"Total: {totalAmount:F2}");
        }
    }

    public class Product
    {
        public string InputLine { get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; }
        public decimal PriceExclusiveTax { get; set; }
        public decimal Tax { get; set; }
        public decimal FinalPrice { get; set; }
    }
}
