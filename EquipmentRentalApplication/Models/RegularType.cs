using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentRentalApplication.Models
{
    public class RegularType : EquipmentType
    {

        public override int LoyaltyPoints { get; set; }
        public override string TypeName { get; set; }

        public override decimal CalculateRentalPrice(uint days) {
            //rental price is one-time rental fee plus premium fee for the first 2 days each plus regular fee for the number of days over 2
            return Fee.OneTime + Fee.Premium * Math.Min(2, days) + Fee.Regular * Math.Max(0, days - 2);
        }
    }
}
