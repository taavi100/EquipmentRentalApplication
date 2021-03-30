using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentRentalApplication.Models
{
    public class SpecializedType : EquipmentType
    {
        public override int LoyaltyPoints { get; set; }
        public override string TypeName { get; set; }

        public override decimal CalculateRentalPrice(uint days) {
            //rental price is premium fee for the first 3 days each plus regular fee times the number of days over 3
            return Fee.Premium * Math.Min(3, days) + Fee.Regular * Math.Max(0, (int)days - 3);
        }
    }
}
