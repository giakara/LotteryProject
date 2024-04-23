using LotteryProject.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace LotteryProject.EFCore.Configurations
{
    public class PresentConfiguration : IEntityTypeConfiguration<Present>
    {
        public void Configure(EntityTypeBuilder<Present> builder)
        {
            builder.ToTable("Presents");
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Description)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(b => b.Category)
                .HasMaxLength(100);


        }
    }
}
