using LotteryProject.EFCore.Services;
using LotteryProject.Models.DTOs;
using LotteryProject.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LotteryProject.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestController : ControllerBase
    {
        private readonly IGuestService _guestService;
        public GuestController(IGuestService guestService)
        {
            _guestService = guestService;
        }

        [HttpGet]
        [Route("GetAllGuests")]
        public async Task<IActionResult> GetAllGuests(CancellationToken cancellationToken = default)
        {
            var allGuests = await _guestService.GetAllGuests(cancellationToken);
            return Ok(allGuests);
        }
        [HttpGet]
        [Route("GetGuest/{guestID:guid}")]
        public async Task<IActionResult> GetById(Guid guestID, CancellationToken cancellationToken = default)
        {
            var guest = await _guestService.GetGuest(guestID, cancellationToken);
            return Ok(guest);
        }
        [HttpPost]
        [Route("AddGuest")]
        public async Task<IActionResult> Add([FromBody] AddGuestDTO guest, CancellationToken cancellationToken = default)
        {
            var guestToAdd = await _guestService.AddGuest(guest, cancellationToken);

            return Ok(guestToAdd);
        }
        [HttpPut]
        [Route("UpdateGuest/{guestID:guid}")]
        public async Task<IActionResult> Update([FromBody] Guest guest, CancellationToken cancellationToken = default)
        {
            var guestToUpdate = await _guestService.UpdateGuest(guest, cancellationToken);

            return Ok(guestToUpdate);
        }
        [HttpDelete]
        [Route("DeleteGuest/{guestID:guid}")]
        public async Task<IActionResult> DeleteById(Guid guestID, CancellationToken cancellationToken = default)
        {
            await _guestService.DeleteGuestById(guestID, cancellationToken);

            return Ok();
        }
    }
}
