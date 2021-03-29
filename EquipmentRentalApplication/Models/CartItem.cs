using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentRentalApplication.Models
{
    public class CartItem
    {
        public int EquipmentId { get; set; }

        public int EquipmentTypeId { get; set; }

        public string EquipmentName { get; set; }

        [Range(0, 999)]
        public int DaysRent {get; set;}

    }
}
