using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentRentalApplication.Models
{
    public class CartItem
    {
        public Equipment Equipment { get; set; }

        [Range(0, 999)]
        public int DaysRent {get; set;}

    }
}
