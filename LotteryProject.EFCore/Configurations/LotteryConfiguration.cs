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
			builder.Property(b => b.GuestId).IsRequired();
			builder.HasOne(b => b.Guest).WithMany(x => x.Lotteries).HasForeignKey(x => x.GuestId).IsRequired().OnDelete(DeleteBehavior.Restrict);

			builder.Property(b => b.PresentId).IsRequired();
			builder.HasOne(b => b.Present).WithOne().HasForeignKey<Lottery>(x => x.PresentId).IsRequired().OnDelete(DeleteBehavior.Restrict);
			builder.Property(b => b.LotteryDate).IsRequired();
		}
	}
}
