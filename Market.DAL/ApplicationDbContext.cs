using Market.DAL.Repositories;
using Market.Domain.Entity;
using Market.Domain.Helpers;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
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
            : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // для связи 1 к 1
            modelBuilder
                .Entity<Cart>()
                .HasOne(a => a.User)
                .WithOne(a => a.Cart)
                .HasForeignKey<User>(c => c.CartId);

            modelBuilder.Entity<Role>()
                       .HasData(
                       new Role() { Id = 1, Name = "admin" },
                       new Role() { Id = 2, Name = "user" }
                       );

            modelBuilder.Entity<User>()
                          .HasData(
                          new User() { Id = 1, Name = "admin", RoleId = 1, Password = HashPasswordHelper.GetHashPassword("12345"), CartId = 1 },
                          new User() { Id = 2, Name = "user123", RoleId = 2, Password = HashPasswordHelper.GetHashPassword("111"), CartId = 2 }
                          );

            modelBuilder.Entity<Category>()
                         .HasData(
                         new Category() { Id = 1, Name = "Фрукты" },
                         new Category() { Id = 2, Name = "Овощи" }
                         );

            modelBuilder.Entity<Product>()
                .HasData(
                new Product()
                {
                    Id = 1,
                    CategoryId = 1,
                    Description = "Яблоко сорта «Карамелька» такое же сладкое, как и его название! Плоды с преобладающим красным румянцем порадуют нежной рассыпчатой мякотью и насыщенным медовым вкусом. «Карамелька» может служить прекрасным перекусом, а также самостоятельным десертом. Кроме того, из таких яблок получаются отличные компоты и варенья.",
                    ImgPath = "/img/apples3.jpg",
                    Name = "Яблоко Карамелька",
                    Price = 50
                },
                new Product()
                {
                    Id = 2,
                    CategoryId = 1,
                    Description = "Банан настолько популярный и любимый многими фрукт, что не нуждается в представлении! Яркий жёлтый цвет, нежная сладкая мякоть и насыщенный аромат делают его безусловным фаворитом среди взрослых и детей. Свежий банан очень питательный, поэтому помогает быстро утолить голод. Съесть его можно на ходу, не запачкав руки, ведь банан очень удобно чистить. Этот фрукт широко применяются в кулинарии. Без них не было бы детского питания, нежных панкейков, творожков, йогуртов и мюсли.",
                    ImgPath = "/img/bannana.jpg",
                    Name = "Банан",
                    Price = 70
                },
                new Product()
                {
                    Id = 3,
                    CategoryId = 2,
                    Description = "Этот овощ популярен и любим во всём мире! Жареные золотистые дольки, ароматное пюре из детства, цельные клубни, сваренные «в мундире» – картофель прекрасен в любых кулинарных проявлениях. Вкус этого продукта столь же уникален, как и он сам. В приготовленном виде он, как правило, имеет мягкую крахмалистую текстуру и сладковатый привкус. Какая бы кулинарная идея с участием картофеля ни пришла вам в голову, воплотить её будет нетрудно!",
                    ImgPath = "/img/potatos.jpg",
                    Name = "Картофель, 400г",
                    Price = 44
                },
                new Product()
                {
                    Id = 4,
                    CategoryId = 2,
                    Description = "Гладкие огурцы, как правило, длиннее обычных и покрыты гладкой глянцевой кожицей. Этот сорт идеально подходит для приготовления различных закусок. Они не горчат, обладают отличным вкусом и выразительным ароматом. Благодаря плотной и в то же время очень нежной мякоти, из них получаются превосходные овощные роллы. Гладкие огурцы удобно нарезать тонкими пластинками, которые легко сворачиваются в рулетики. В качестве начинки попробуйте тунец, лосось или творог с чесноком и зеленью.",
                    ImgPath = "/img/cucmbers.jpg",
                    Name = "Огурцы, 200г",
                    Price = 30
                },
                new Product()
                {
                    Id = 5,
                    CategoryId = 1,
                    Description = "Мандарины любят многие, ведь они дарят ощущение ностальгии по детству. Без их вкуса и аромата просто не существует новогоднего стола и праздничного настроения! Эти маленькие цитрусы имеют сочную мякоть и искристый кисло-сладкий вкус. Разделить такой фрукт с кем-то приятным – что может быть лучше! А еще с мандарином можно смело экспериментировать в кулинарии. Попробуйте приготовить лёгкий салат, добавив к мандарину камамбер и руколу.",
                    ImgPath = "/img/mandarin.jpg",
                    Name = "Мандарины, 100г",
                    Price = 70
                },
                  new Product()
                  {
                      Id = 6,
                      CategoryId = 1,
                      Description = "Легендарное египетское манго снова в городе!\r\nСладко-сочное, безволокнистое, с нежной бархатистой текстурой. Обманчиво зелёное снаружи, но идеально спелое внутри. Манго уже можно купить в наших магазинах или заказать на дом с бесплатной доставкой.\r\nВес одного манго — от 500 грамм. ",
                      ImgPath = "/img/mango.jpg",
                      Name = "Манго, 1 шт",
                      Price = 90
                  }
                );

            modelBuilder.Entity<Cart>()
                .HasData(
                new Cart() { Id = 1, UserId = 1 },
                new Cart() { Id = 2, UserId = 2 }
                );

            modelBuilder.Entity<CartItem>()
                .HasData(
                new CartItem() { Id = 1, CartId = 2, Count = 1, ProductId = 5 },
                new CartItem() { Id = 2, CartId = 1, Count = 1, ProductId = 4 },
                new CartItem() { Id = 3, CartId = 1, Count = 2, ProductId = 2 }
                );

        }
    }
}

