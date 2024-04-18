using LotteryProject.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace LotteryProject.EFCore.Configurations
{
    public class LotteryConfiguration : IEntityTypeConfiguration<Lottery>
    {
        public void Configure(EntityTypeBuilder<Lottery> builder)
        {
            builder.ToTable("Lotteries");
            builder.HasKey(b => b.Id);


        }
    }
}
