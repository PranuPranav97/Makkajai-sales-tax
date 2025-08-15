namespace SalesTaxNS
{
public class Receipt
{
    private readonly List<Product> products;
    private decimal TotalAmount;
    private decimal TotalTax;
    public Receipt(List<Product> products)
    {
        this.products = products;
        this.CalculateTotaAmountAndTotalTax();
    }

    //Loop only once to calculate the total amount and total tax
    private void CalculateTotaAmountAndTotalTax()
    {
        decimal totalAmount = 0;
        decimal totalTax = 0;

        foreach (var p in products)
        {

            totalTax += p.Tax;
            totalAmount += p.FinalPrice;
        }
        this.TotalAmount = totalAmount;
        this.TotalTax = totalTax;
    }



        // Responsible for printing the receipt to console.
        public void Print()
        {
            foreach (var p in products)
            {
                Console.WriteLine($"{p.Quantity} {p.Name}: {p.FinalPrice:F2}");

            }
            Console.WriteLine($"Sales Taxes: {TotalTax:F2}");
            Console.WriteLine($"Total: {this.TotalAmount:F2}");
        }
    }

}