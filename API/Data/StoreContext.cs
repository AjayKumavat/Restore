using System;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class StoreContext(DbContextOptions options) : DbContext(options)
{
    public required DbSet<Product> Products { get; set; }

    //Note:
    //We've mentioned BasketItem property in Basket class
    //So we don't need to add DbSet for BasketItem here
    //EF Core will create the table automatically
    //But need to explicitly add the name for BasketItem table in OnModelCreating method or by using Data Annotations.
    public required DbSet<Basket> Baskets { get; set; }
}
