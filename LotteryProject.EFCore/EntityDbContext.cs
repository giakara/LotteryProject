using Microsoft.EntityFrameworkCore;
using LotteryProject.Models;
using System.Collections.Generic;
using LotteryProject.Models.Entities;

namespace LotteryProject.EFCore
{
    public static class EntityDbContext
    {
        public class DataContext : DbContext
        {
            public DataContext(DbContextOptions<DataContext> options) : base(options) { }

            public DbSet<Guest> Guests { get; set; }
            public DbSet<Present> Presents { get; set; }
            public DbSet<Lottery> Lotteries { get; set; }
        }
    }
}