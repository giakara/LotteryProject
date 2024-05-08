using LotteryProject.Models.Entities;
using Microsoft.EntityFrameworkCore;
using static LotteryProject.EFCore.EntityDbContext;

namespace LotteryProject.UnitTests.Fixtures
{
	public class DBFixture
	{
		private const string ConnectionString = "Data Source=localhost;Initial Catalog=TestLotteryProject;User ID=sa; Password=Lottery!123;TrustServerCertificate=true;";

		private static readonly object _lock = new();
		private static bool _databaseInitialized;

		public DBFixture()
		{
			lock (_lock)
			{
				if (!_databaseInitialized)
				{
					using (var context = CreateContext())
					{
						context.Database.EnsureDeleted();
						context.Database.EnsureCreated();
						context.AddRange(
							new Guest("test1", "test1")
							{
								Id = new Guid("3a39a232-b5f1-4f04-b07b-43c843185889")
							},
							new Guest("test2", "test2")
							{
								Id = new Guid("dd3f3be4-1094-459e-b3a6-a978aaf550a3")
							},
							new Present("testPresent1")
							{
								Id = new Guid("1ba7e5d5-69e1-4888-8e83-3e060d11e83c")
							},
							new Present("testPresent2")
							{
								Id = new Guid("50530ff8-9798-4dda-8a22-41ab36b5a0fa")
							},
							new Present("testPresent2")
							{
								Id = new Guid("fba7c0dc-4aba-45dd-aab2-dec90db19be8")
							},
						new Lottery(new Guid("1ba7e5d5-69e1-4888-8e83-3e060d11e83c"))
						{
							GuestId = new Guid("3a39a232-b5f1-4f04-b07b-43c843185889"),
							LotteryDate = DateTime.Now,
						},
						new Lottery(new Guid("1ba7e5d5-69e1-4888-8e83-3e060d11e83c"))
						{
							GuestId = new Guid("dd3f3be4-1094-459e-b3a6-a978aaf550a3"),
							LotteryDate = DateTime.Now,
						});
						context.SaveChanges();
					}

					_databaseInitialized = true;
				}
			}
		}

		public DataContext CreateContext()
			=> new DataContext(
				new DbContextOptionsBuilder<DataContext>()
					.UseSqlServer(ConnectionString)
					.Options);
	}
}
