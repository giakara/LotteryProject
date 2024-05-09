
using LotteryProject.Models.DTOs;
using LotteryProject.Models.Entities;
using LotteryProject.Models.Paging;
using System.Threading.Tasks;

namespace LotteryProject.Client.Shared.Services.Interfaces
{
	public interface IEmailService
	{
		Task SendEmail(EmailData emailData, CancellationToken cancellationToken = default);

	}
}
