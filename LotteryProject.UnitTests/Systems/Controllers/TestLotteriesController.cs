using LotteryProject.EFCore.Services;
using LotteryProject.Models.DTOs;
using LotteryProject.Models.Entities;
using LotteryProject.Server.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryProject.UnitTests.Systems.Controllers
{
    public class TestLotteriesController
    {
        private readonly Mock<ILotteryService> _mocklotteryService;

        public TestLotteriesController()
        {
            _mocklotteryService = new Mock<ILotteryService>();
        }
        //[Fact]
        //public async Task Test_CreateLottery_Failed_ThrowLotteryPresentIsGiftedException()
        //{
        //    var guid = Guid.NewGuid();
        //    var cancellationToken = new CancellationToken();
        //    var lotterytoAdd = new AddEditLotteryDTO
        //    {
        //        PresentID = guid,
        //    };
        //    //Arrange
        //    _mocklotteryService.Setup(service => service.CreateLottery(lotterytoAdd, cancellationToken))
        //        .ReturnsAsync(new Lottery());
        //}
    }
}
