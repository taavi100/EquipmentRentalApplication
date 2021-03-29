using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EquipmentRentalApplication.Models
{
    public class LineItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LineItemId { get; set; }
        [ForeignKey("Equipment"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EquipmentId { get; set; }
        public virtual Equipment Equipment { get; set; }

        [ForeignKey("Invoice"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int InvoiceId { get; set; }
        public virtual Invoice Invoice { get; set; }
        public int DaysRent { get; set; }
    }
}
