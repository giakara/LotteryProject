

using LotteryProject.Models.DTOs;
using LotteryProject.Models.Entities;
using LotteryProject.Models.Paging;

namespace LotteryProject.EFCore.Services
{
	public interface IEmailService
	{
		Task SendEmailAsync(EmailData emaildata, CancellationToken cancellationToken = default);
	}
}
