using Market.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();

            //Category fruitCategory = new Category() { Name = "Фрукты" };
            //Category vegCategory = new Category() { Name = "Овощи" };

            //Product apple = new Product()
            //{ Name = "Яблоко", Description = "Вкусное яблоко", Price = 50, Category = fruitCategory, ImgPath="/img/apple-red.jpg" };

            //Product bananna = new Product()
            //{ Name = "Банан", Description = "Спелый банан", Price = 70, Category = fruitCategory, ImgPath = "/img/bannana.jpg" };

            //Product potato = new Product()
            //{ Name = "Картофель", Description = "Молодой картофель", Price = 30, Category = vegCategory, ImgPath = "/img/potato.jpg" };

            //this.Products.AddRange(apple, bananna, potato);
            //this.SaveChanges();

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Product>().HasData(
            //        new Product { Id = 1, Name = "Банан", Description = "111", Price = 111 }
            //new Product { Id = 2, Name = "Булочка с корицей" },
            //new Product { Id = 3, Name = "Морковка" }
            //);
        }

    }
}
