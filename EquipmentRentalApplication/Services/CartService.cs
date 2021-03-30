using EquipmentRentalApplication.Dal;
using EquipmentRentalApplication.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EquipmentRentalApplication.Services
{
    public class CartService : ICartService
    {
        private readonly EquipmentContext _context;

        public CartService(EquipmentContext context)
        {
            _context = context;
        }

        public async Task<Invoice> SaveCart(Cart cart)
        {
            var customer = await _context.Customers.FindAsync(cart.Client.CustomerId);
            
            Invoice invoice = new Invoice { Customer = customer, CustomerId = customer.CustomerId, Title = $"Invoice {customer.CustomerName}" };

            var items = cart.CartItem
                .Select(item => new LineItem { EquipmentId = item.EquipmentId, DaysRent = item.DaysRent })
                .Where(item => item.DaysRent > 0)
                .ToList();

            invoice.OrderItem = items;
            await _context.Invoice.AddAsync(invoice);
            await _context.SaveChangesAsync();

            return invoice;
        }

        public async Task<string> GetInvoiceAsync(Invoice invoice)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("ee-EE");
            int bonusPoints = 0;
            decimal totalPrice = 0.0M;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(invoice.Title);
            sb.AppendLine(new string('_', 40));

            using (StringWriter sw = new StringWriter(sb))
            {
                foreach (var item in invoice.OrderItem)
                {

                    decimal itemPrice = item.Equipment.Type.CalculateRentalPrice((uint)item.DaysRent);
                    bonusPoints += item.Equipment.Type.LoyaltyPoints;
                    totalPrice += itemPrice;
                    await sw.WriteLineAsync($"{item.Equipment.EquipmentName,25}{item.DaysRent,5}{itemPrice,10:C2}");
                }
                await sw.WriteLineAsync(new string('_', 40));
                await sw.WriteLineAsync($"Total price:   {totalPrice,25:C2}");
                await sw.WriteLineAsync($"Bonus points:  {bonusPoints,25}");
            }
            
            return sb.ToString();

            
        }
    }
}
