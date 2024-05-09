using LotteryProject.EFCore.Services;
using LotteryProject.Models.DTOs;
using LotteryProject.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.Net;
using System.Net.Mail;

namespace LotteryProject.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EmailController : ControllerBase
	{
		private readonly IEmailService _emailService;
		public EmailController(IEmailService emailService)
		{
			_emailService = emailService;
		}
		[HttpPost]
		[Route("SendEmail")]
		public async Task<IActionResult> SendEmail([FromBody] EmailData emaildata, CancellationToken cancellationToken = default)
		{
			await _emailService.SendEmailAsync(emaildata, cancellationToken);

			return Ok();
		}
	}
}
