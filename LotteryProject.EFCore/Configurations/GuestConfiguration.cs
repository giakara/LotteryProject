using LotteryProject.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace LotteryProject.EFCore.Configurations
{
    public class GuestConfiguration : IEntityTypeConfiguration<Guest>
    {
        public void Configure(EntityTypeBuilder<Guest> builder)
        {
            builder.ToTable("Guests");
            builder.HasKey(b => b.Id);

            builder.Property(b => b.GuestName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(b => b.GuestSurname)
                .HasMaxLength(100)
                .IsRequired();

        }
    }
}
