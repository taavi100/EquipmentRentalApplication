using EquipmentRentalApplication.Models;
using System.Threading.Tasks;

namespace EquipmentRentalApplication.Services
{
    public interface ICartService
    {
        Task<Invoice> SaveCart(Cart cart);
        Task<string> GetInvoiceAsync(Invoice invoice);
    }
}
