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

            builder.HasOne(b => b.Guest)
            .WithOne(b => b.Lottery)
            .HasForeignKey<Guest>(b => b.Id)
            .OnDelete(DeleteBehavior.Cascade)// Cascade delete if a TodoItem is deleted
            .IsRequired();

            builder.HasOne(b => b.Present)
            .WithOne(b => b.Lottery)
            .HasForeignKey<Present>(b => b.Id)
            .OnDelete(DeleteBehavior.Cascade)// Cascade delete if a TodoItem is deleted
            .IsRequired();



        }
    }
}
