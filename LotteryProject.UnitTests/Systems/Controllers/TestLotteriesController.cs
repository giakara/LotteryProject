using FluentAssertions;
using LotteryProject.EFCore.Services;
using LotteryProject.Models.DTOs;
using LotteryProject.Models.Entities;
using LotteryProject.Models.Exceptions;
using LotteryProject.Server.Controllers;
using LotteryProject.UnitTests.Fixtures;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public async void Test_CreateLottery_Failed_ThrowLotteryNoAvailableGuestsException()
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
        public async void Test_CreateLottery_Success()
        {
            using var context = _fixture.CreateContext();
            var guestGuid = new Guid("c1ec69af-1e22-4a3f-a959-5772a8a1dc2e");
            context.AddRange(new Guest("test3", "test3")
            {
                Id = guestGuid,
            });
            context.SaveChanges();
            var guid = new Guid("fba7c0dc-4aba-45dd-aab2-dec90db19be8");
            var cancellationToken = new CancellationToken();
            var lotterytoAdd = new AddEditLotteryDTO
            {
                PresentID = guid,
            };

            var lotteryService = new LotteryService(context);
            var successReturn = lotteryService.CreateLottery(lotterytoAdd, cancellationToken);
            Assert.Equal(successReturn.Result.PresentID, lotterytoAdd.PresentID);

        }
    }
}
