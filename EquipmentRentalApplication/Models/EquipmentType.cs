
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EquipmentRentalApplication.Models
{
    public abstract class EquipmentType : ICalculateRentalPrice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TypeId { get; set; }

        public abstract int LoyaltyPoints { get; set; }

        public abstract string TypeName { get; set; }

        public virtual ICollection<Equipment> EquipmentList { get; set; }
        public abstract decimal CalculateRentalPrice(uint days);
    }
}
