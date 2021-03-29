using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentRentalApplication.Models
{
    public class Cart
    {
        public Customer Client { get; set; }
        public ICollection<CartItem> CartItem { get; set; }

    }
}
