using EquipmentRentalApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace EquipmentRentalApplication.Dal
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            //Equipment type
            modelBuilder.Entity<HeavyType>().HasData(new HeavyType { TypeId = 1, TypeName = "Heavy type", LoyaltyPoints = 2 });
            modelBuilder.Entity<SpecializedType>().HasData(new SpecializedType { TypeId = 2, TypeName = "Specialized type", LoyaltyPoints = 1 });
            modelBuilder.Entity<RegularType>().HasData(new RegularType { TypeId = 3, TypeName = "Regular type", LoyaltyPoints = 1 });

            //Equipments
            modelBuilder.Entity<Equipment>().HasData(
                new Equipment { EquipmentId = 1, EquipmentName = "Caterpillar bulldozer", EquipmentTypeId = 1 },
                new Equipment { EquipmentId = 2, EquipmentName = "KamAZ truck", EquipmentTypeId = 3 },
                new Equipment { EquipmentId = 3, EquipmentName = "Komatsu crane", EquipmentTypeId = 1 },
                new Equipment { EquipmentId = 4, EquipmentName = "Volvo steamroller", EquipmentTypeId = 3 },
                new Equipment { EquipmentId = 5, EquipmentName = "Bosch jackhammer", EquipmentTypeId = 2 });

            //Customer
            modelBuilder.Entity<Customer>().HasData(new Customer { CustomerId = 1, CustomerName = "Default User" });
        }
    }
}
