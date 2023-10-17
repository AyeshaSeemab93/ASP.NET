using System;
using EntityFrameWorkNET23KA.Models;
using System.Linq; // Import System.Linq for LINQ
using var db = new TutorialDbContext();

Console.Write("Enter the customer name: ");
string inputName = Console.ReadLine();

var customer = db.Customers
    .Where(c => c.Name == inputName)
    .FirstOrDefault(); // Use FirstOrDefault to handle potential null results(when no such name found)

if (customer != null)
{
    Console.WriteLine("Customer Details:");
    Console.WriteLine("Name: " + customer.Name);
    Console.WriteLine("Location: " + customer.Location);
    Console.WriteLine("ID: " + customer.CustomerId);
    Console.WriteLine("Email: " + customer.Email);
}
else
{
    Console.WriteLine("Customer not found.");
}
