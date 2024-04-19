

using FluentAssertions;
using LotteryProject.EFCore.Services;
using LotteryProject.Models.Entities;
using LotteryProject.Server.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace LotteryProject.UnitTests.Systems.Controllers
{
    public class TestGuestsController
    {
        private readonly Mock<IGuestService> _mockGuestService;

        public TestGuestsController()
        {
            _mockGuestService = new Mock<IGuestService>();
        }
        [Fact]
        public async Task Get_OnSuccess_ReturnStatusCode_200()
        {
            var cancellationToken = new CancellationToken();
            //Arrange
            _mockGuestService.Setup(service => service.GetAllGuests(cancellationToken))
                .ReturnsAsync(new List<Guest>());
            var guestsController = new GuestController(_mockGuestService.Object);
            //Act
            var result = (OkObjectResult)await guestsController.GetAllGuests();
            //Assert
            result.StatusCode.Should().Be(200);
        }
    }
}
