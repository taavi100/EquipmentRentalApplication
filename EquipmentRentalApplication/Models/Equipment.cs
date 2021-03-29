using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EquipmentRentalApplication.Models
{
    public class Equipment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EquipmentId { get; set; }
        public string EquipmentName { get; set; }

        [ForeignKey("EquipmentType"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EquipmentTypeId { get; set; }
        public virtual EquipmentType Type { get; set; }
    }
}
