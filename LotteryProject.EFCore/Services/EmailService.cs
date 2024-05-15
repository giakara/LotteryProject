using LotteryProject.Models.DTOs;
using LotteryProject.Models.Entities;
using LotteryProject.Models.Exceptions;
using LotteryProject.Models.Paging;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;


namespace LotteryProject.EFCore.Services
{
	public class EmailService : IEmailService
	{

		public Task SendEmailAsync(EmailData emaildata, CancellationToken cancellationToken = default)
		{
			string mailFrom = "jadyn.becker@ethereal.email";
			string password = "BbaGdRUPnFTNdVCWES";

			var client = new SmtpClient("smtp.ethereal.email", 587)
			{
				EnableSsl = true,
				Credentials = new NetworkCredential(mailFrom, password)
			};
			return client.SendMailAsync(new MailMessage(from: mailFrom,
									to: emaildata.EmailAddress,
									emaildata.Subject, emaildata.Message), cancellationToken);
		}
	}
}