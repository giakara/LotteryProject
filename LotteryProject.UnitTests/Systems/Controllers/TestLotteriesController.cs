using FluentAssertions;
using LotteryProject.EFCore.Services;
using LotteryProject.Models.DTOs;
using LotteryProject.Models.Entities;
using LotteryProject.Models.Exceptions;
using LotteryProject.Server.Controllers;
using LotteryProject.UnitTests.Fixtures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static LotteryProject.EFCore.EntityDbContext;
using static System.Collections.Specialized.BitVector32;

namespace LotteryProject.UnitTests.Systems.Controllers
{
	public class TestLotteriesController : IClassFixture<DBFixture>
	{
		private readonly Mock<ILotteryService> _mocklotteryService;
		private DBFixture _fixture;
		public TestLotteriesController(DBFixture fixture)
		{
			_mocklotteryService = new Mock<ILotteryService>();
			_fixture = fixture;

		}
		[Fact]
		public async void Test_CreateLottery_Failed_ThrowLotteryPresentIsGiftedException()
		{
			using var context = _fixture.CreateContext();
			var guid = new Guid("1ba7e5d5-69e1-4888-8e83-3e060d11e83c");
			var cancellationToken = new CancellationToken();
			var lotterytoAdd = new AddEditLotteryDTO
			{
				PresentID = guid,
			};

			var lotteryService = new LotteryService(context);
			await Assert.ThrowsAsync<LotteryPresentIsGiftedException>(() => lotteryService.CreateLottery(lotterytoAdd, cancellationToken));

		}
		[Fact]
		public async void Test_CreateLottery_Failed_ThrowLotteryPresentIsRequiredException()
		{
			using var context = _fixture.CreateContext();
			var guid = Guid.Empty;
			var cancellationToken = new CancellationToken();
			var lotterytoAdd = new AddEditLotteryDTO
			{
				PresentID = guid,
			};
			var lotteryService = new LotteryService(context);
			await Assert.ThrowsAsync<LotteryPresentIsRequiredException>(() => lotteryService.CreateLottery(lotterytoAdd, cancellationToken));

		}
		[Fact]
		public async Task Test_CreateLottery_Failed_ThrowLotteryNoAvailableGuestsException()
		{
			using var context = _fixture.CreateContext();
			var guid = new Guid("fba7c0dc-4aba-45dd-aab2-dec90db19be8");
			var cancellationToken = new CancellationToken();
			var lotterytoAdd = new AddEditLotteryDTO
			{
				PresentID = guid,
			};

			var lotteryService = new LotteryService(context);
			await Assert.ThrowsAsync<LotteryNoAvailableGuestsException>(() => lotteryService.CreateLottery(lotterytoAdd, cancellationToken));

		}
		[Fact]
		public async Task Test_CreateLottery_Success()
		{
			var cancellationToken = new CancellationToken();
			using var context = _fixture.CreateContext();
			var guestGuid = new Guid("c1ec69af-1e22-4a3f-a959-5772a8a1dc2e");
			var newGuest = new Guest("test3", "test3", "ggg@net.gr")
			{
				Id = guestGuid,
			};
			var guid = new Guid("fba7c0dc-4aba-45dd-aab2-dec90db19be8");
			try
			{
				await context.AddRangeAsync(newGuest);
				await context.SaveChangesAsync();


				var lotterytoAdd = new AddEditLotteryDTO
				{
					PresentID = guid,
				};

				var lotteryService = new LotteryService(context);
				var successReturn = lotteryService.CreateLottery(lotterytoAdd, cancellationToken)?.Result;
				successReturn!.PresentId.Should().Be(guid);
			}
			finally
			{
				context.Set<Guest>().Remove(newGuest);
				context.Set<Lottery>().Remove(context.Lotteries.First(x => x.PresentId == guid));
				await context.SaveChangesAsync();

			}
			//var posts = new List<Guest>();
			//var contextMock = new Mock<DataContext>();
			//var guestGuid = new Guid("c1ec69af-1e22-4a3f-a959-5772a8a1dc2e");
			//var newGuest = new Guest("test3", "test3")
			//{
			//    Id = guestGuid,
			//};
			//var guid = new Guid("fba7c0dc-4aba-45dd-aab2-dec90db19be8");
			//var lotterytoAdd = new AddEditLotteryDTO
			//{
			//    PresentID = guid,
			//};
			//var present = new Present("tessss") { Id = guid };

			//posts.Add(newGuest);
			//contextMock.Setup(p => p.Guests).ReturnsDbSet(posts);
			//contextMock.Setup(p => p.Lotteries).ReturnsDbSet(new List<Lottery>());
			//contextMock.Setup(p => p.Presents).ReturnsDbSet(new List<Present>() { present });
			//var lotteryService = new LotteryService(contextMock.Object);
			//var successReturn = lotteryService.CreateLottery(lotterytoAdd, cancellationToken)?.Result;
			//successReturn!.PresentID.Should().Be(guid);




		}
	}
}
