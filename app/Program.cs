using System;
using System.Collections.Generic;
using System.Globalization;
namespace SalesTaxNS
{
    public class Program
    {
        public static void Main(string[] args)
        {

            List<Product> products = new List<Product>();
            ProductBuilder prodBuilder = new ProductBuilder();
            while (true)
            {
                string inputLine = Console.ReadLine()?? string.Empty;
                if (string.IsNullOrWhiteSpace(inputLine))
                    break;

                products.Add(prodBuilder.BuildProductFromInput(inputLine));
            }
            if (products == null || products.Count == 0)
            {
                throw new ArgumentException("Products cannot be null or empty.", "products");
            }

            Receipt receipt = new Receipt(products);
            receipt.Print();
        }
    }
}