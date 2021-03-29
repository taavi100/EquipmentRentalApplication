using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentRentalApplication.Models
{
    public class HeavyType : EquipmentType
    {
        public override int LoyaltyPoints { get; set; }
        public override string TypeName { get; set; }

        public override decimal CalculateRentalPrice(uint days) {
            //rental price is one-time rental fee plus premium fee for each day rented
            return Fee.OneTime + Fee.Premium * days;
        }
    }
}
