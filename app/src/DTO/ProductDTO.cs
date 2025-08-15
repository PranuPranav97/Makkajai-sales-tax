 
 namespace SalesTaxNS
{
 public class Product
    {
        public required string InputLine { get; set; }
        public int Quantity { get; set; }
        public required string Name { get; set; }
        public decimal PriceExclusiveTax { get; set; }
        public decimal Tax { get; set; }
        public decimal FinalPrice { get; set; }
    }
}