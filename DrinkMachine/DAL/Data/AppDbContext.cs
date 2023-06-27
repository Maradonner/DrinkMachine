using DrinkMachine.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DrinkMachine.DAL.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Drink> Drinks { get; set; }
    public DbSet<Coin> Coins { get; set; }
    public DbSet<SessionModel> DbSessions { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coin>().HasData(new Coin
            {
                Id = 1,
                IsBlocked = false,
                Value = 1,
                ImageUrl = "https://ru-moneta.ru/upload/monety-ru/rubl-1-1.jpg"
        },
            new Coin
            {
                Id = 2,
                IsBlocked = false,
                Value = 2,
                ImageUrl = "https://ru-moneta.ru/upload/monety-ru/st-1-4.jpg"

            },
            new Coin
            {
                Id = 3,
                IsBlocked = false,
                Value = 5,
                ImageUrl = "https://ru-moneta.ru/upload/monety-ru/1999-sr.jpg"
            },
            new Coin
            {
                Id = 4,
                IsBlocked = false,
                Value = 10,
                ImageUrl = "https://ru-moneta.ru/upload/moneta-ru/2019-10rub-01r.jpg"
            }
        );

        modelBuilder.Entity<Drink>().HasData(
            new Drink
            {
                Id = 1,
                Name = "Pepsi",
                Price = 15,
                Quantity = 10,
                ImageUrl = "https://cdn.iportal.ru/preview/news/articles/1abee4eeb5843831b81b268032278c150f1c079a_1200_800_c.jpg"

            },
            new Drink
            {
                Id = 2,
                Name = "Fanta",
                Price = 17,
                Quantity = 15,
                ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR0cdopkxHgUUq2rrnqIVRMCXYbyQD0f2n7-A&usqp=CAU"
            },
            new Drink
            {
                Id = 3,
                Name = "Dobriy Cola",
                Price = 12,
                Quantity = 121,
                ImageUrl = "https://lenta.servicecdn.ru/globalassets/1/-/65/86/05/438102.png?preset=fulllossywhite"
            }
        );

        base.OnModelCreating(modelBuilder);
    }
}