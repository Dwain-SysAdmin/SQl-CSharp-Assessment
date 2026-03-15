using CustomerOrders.Helpers;
using Microsoft.Data.SqlClient;

Console.WriteLine("Customer Order Summary");
Console.WriteLine("----------------------");

try
{

    Console.Write("Minimum Spend Filter (or press enter): ");

    string? input = Console.ReadLine();

    decimal? minSpend = null;

    if (!string.IsNullOrWhiteSpace(input))
    {
        minSpend = Convert.ToDecimal(input);

        if (minSpend < 0)
        {
            Console.WriteLine($"[{DateTime.Now}] Error: minSpend must be > 0");
            return;
        }
    }

    Database _ = new();

    var customers = Database.GetCustomerSummary(minSpend);

    if (customers.Count == 0)
    {
        Console.WriteLine("No Customers found.");
        return;
    }

    Console.WriteLine("Customer Results");
    Console.WriteLine("================");

    foreach (var c in customers)
    {
        Console.WriteLine($"Customer: {c.CustomerName}");
        Console.WriteLine($"Orders: {c.TotalOrders}");
        Console.WriteLine($"Total Spend: {c.TotalSpend:C}");
        Console.WriteLine("----------------------------");
    }

    Console.WriteLine("Done.");

}
catch (FormatException)
{
    Console.WriteLine($"[{DateTime.Now}] Invalid number entered.");
}
catch (SqlException)
{
    Console.WriteLine($"[{DateTime.Now}] Database connection error.");
}
catch (Exception ex)
{
    Console.WriteLine($"[{DateTime.Now}] Unexpected error: {ex.Message}");
}