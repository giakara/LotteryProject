using LotteryProject.EFCore.Services;
using LotteryProject.Models.DTOs;
using LotteryProject.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LotteryProject.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PresentController : ControllerBase
    {
        private readonly IPresentService _presentService;
        public PresentController(IPresentService presentService)
        {
            _presentService = presentService;
        }

        [HttpGet]
        [Route("GetAllPresents")]
        public async Task<IActionResult> GetAllPresents(CancellationToken cancellationToken = default)
        {
            var allPresents = await _presentService.GetAllPresents(cancellationToken);
            return Ok(allPresents);
        }
        [HttpGet]
        [Route("GetPresent/{presentID:guid}")]
        public async Task<IActionResult> GetById(Guid presentID, CancellationToken cancellationToken = default)
        {
            var present = await _presentService.GetPresent(presentID, cancellationToken);
            return Ok(present);
        }
        [HttpPost]
        [Route("AddPresent")]
        public async Task<IActionResult> Add([FromBody] AddPresentDTO present, CancellationToken cancellationToken = default)
        {
            var presentToAdd = await _presentService.AddPresent(present, cancellationToken);

            return Ok(presentToAdd);
        }
        [HttpPut]
        [Route("UpdatePresent/{presentID:guid}")]
        public async Task<IActionResult> Update([FromBody] Present present, CancellationToken cancellationToken = default)
        {
            var presentToUpdate = await _presentService.UpdatePresent(present, cancellationToken);

            return Ok(presentToUpdate);
        }
        [HttpDelete]
        [Route("DeletePresent/{presentID:guid}")]
        public async Task<IActionResult> DeleteById(Guid presentID, CancellationToken cancellationToken = default)
        {
            await _presentService.DeletePresentById(presentID, cancellationToken);

            return Ok();
        }
    }
}
