using Microsoft.EntityFrameworkCore;
using LotteryProject.Models;
using System.Collections.Generic;
using LotteryProject.Models.Entities;
using LotteryProject.EFCore.Configurations;

namespace LotteryProject.EFCore
{
    public static class EntityDbContext
    {
        public class DataContext : DbContext
        {
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
                new GuestConfiguration().Configure(modelBuilder.Entity<Guest>());
                new PresentConfiguration().Configure(modelBuilder.Entity<Present>());
                new LotteryConfiguration().Configure(modelBuilder.Entity<Lottery>());
            }
            public DataContext(DbContextOptions<DataContext> options) : base(options) { }

            public DbSet<Guest> Guests { get; set; }
            public DbSet<Present> Presents { get; set; }
            public DbSet<Lottery> Lotteries { get; set; }
        }
    }
}