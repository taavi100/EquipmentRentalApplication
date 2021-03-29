using EquipmentRentalApplication.Dal;
using EquipmentRentalApplication.Models;
using System.IO;
using System.Linq;
using System.Text;
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
            
            Invoice invoice = new Invoice { Customer = customer, Title = $"Invoice {customer.CustomerName}" };
            var items = cart.CartItem
                .Select(item => new LineItem { Equipment = item.Equipment, DaysRent = item.DaysRent })
                .ToList();
            customer.LoyaltyPoints += items
                .Sum(item => item.Equipment.Type.LoyaltyPoints);

            invoice.OrderItem = items;
            await _context.Invoice.AddAsync(invoice);
            await _context.SaveChangesAsync();

            return invoice;
        }

        public async Task<string> GetInvoiceAsync(Invoice invoice)
        {
            int bonusPoints = 0;
            decimal totalPrice = 0.0M;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(invoice.Title);
            sb.AppendLine(new string('_', 25));

            using (StringWriter sw = new StringWriter(sb))
            {
                foreach (var item in invoice.OrderItem)
                {
                    decimal itemPrice = item.Equipment.Type.CalculateRentalPrice((uint)item.DaysRent);
                    bonusPoints += item.Equipment.Type.LoyaltyPoints;
                    totalPrice += itemPrice;
                    await sw.WriteAsync($"{item.Equipment.EquipmentName,10} {item.DaysRent,5} {itemPrice,10:C}");
                }
                await sw.WriteAsync($"Total price: {totalPrice,10:C} Bonus points: {bonusPoints}");
            }
            
            return sb.ToString();

            
        }
    }
}
