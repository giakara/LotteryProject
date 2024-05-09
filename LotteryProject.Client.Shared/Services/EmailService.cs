using LotteryProject.Client.Shared.Services.Interfaces;
using LotteryProject.Models.DTOs;
using LotteryProject.Models.Entities;
using LotteryProject.Models.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace LotteryProject.Client.Shared.Services
{
	public class EmailService : IEmailService
	{
		private readonly HttpClient _httpClient;
		private const string EmailControllerName = "https://localhost:7292/api/Email";
		private readonly JsonSerializerOptions _jsonSerializerOptions;
		public EmailService(HttpClient httpClient)
		{
			_httpClient = httpClient;
			_jsonSerializerOptions = new JsonSerializerOptions()
			{
				PropertyNameCaseInsensitive = true
			};

		}

		public async Task SendEmail(EmailData emailData, CancellationToken cancellationToken = default)
		{
			cancellationToken.ThrowIfCancellationRequested();
			var httpResponse = await _httpClient.PostAsJsonAsync(
									requestUri: $"{EmailControllerName}/SendEmail/",
									value: emailData,
									cancellationToken: cancellationToken);
			httpResponse.EnsureSuccessStatusCode();

			if (!httpResponse.IsSuccessStatusCode)
			{
				var errorMessage = await httpResponse.Content.ReadAsStringAsync();
				throw new HttpRequestException($"Failed to send email. Status code: {httpResponse.StatusCode}. Error: {errorMessage}");
			}
		}
	}
}
