using FoodAppAngular.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodAppAngular.Data
{
    public class FoodItemsDBContext: DbContext
    {
        public FoodItemsDBContext(DbContextOptions<FoodItemsDBContext> options) : base(options)
        {
        }
        public DbSet<Food> Foods { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Food>().HasData(
                new Food { id = 1, name = "Dose", price = 10, category = "Breakfast", imageUrl = "FoodImages/dosa.jpeg" },
                new Food { id = 2, name = "Idli", price = 8, category = "Breakfast", imageUrl = "FoodImages/idli.jpg" },
                new Food { id = 3, name = "Chapathi", price = 15, category = "Indian", imageUrl = "FoodImages/chapathi.JPG" }
            );
        }


    }
}
