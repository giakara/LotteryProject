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
			var guid = new Guid("50530ff8-9798-4dda-8a22-41ab36b5a0fa");
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
			var guid = new Guid("50530ff8-9798-4dda-8a22-41ab36b5a0fa");
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

				context.Set<Lottery>().Remove(context.Lotteries.First(x => x.PresentId == guid));
				await context.SaveChangesAsync();
				context.Set<Guest>().Remove(newGuest);
				await context.SaveChangesAsync();

			}

		}
	}
}
