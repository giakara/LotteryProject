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
	public class LotteryService : ILotteryService
	{
		private readonly HttpClient _httpClient;
		private const string LotteryControllerName = "https://localhost:7292/api/Lottery";
		private readonly JsonSerializerOptions _jsonSerializerOptions;
		public LotteryService(HttpClient httpClient)
		{
			_httpClient = httpClient;
			_jsonSerializerOptions = new JsonSerializerOptions()
			{
				PropertyNameCaseInsensitive = true
			};

		}
		public async Task<Lottery> CreateLottery(AddEditLotteryDTO lottery, CancellationToken cancellationToken = default)
		{
			cancellationToken.ThrowIfCancellationRequested();

			var httpResponse = await _httpClient.PostAsJsonAsync(
									requestUri: $"{LotteryControllerName}/CreateLottery/",
									value: lottery,
									cancellationToken: cancellationToken);

			if (httpResponse.IsSuccessStatusCode)
			{
				var responseBody = await httpResponse.Content.ReadAsStreamAsync();
				var jsonSerializerOptions = new JsonSerializerOptions()
				{
					PropertyNameCaseInsensitive = true,
				};
				var addedItem = await JsonSerializer.DeserializeAsync<Lottery>(responseBody, jsonSerializerOptions);

				return addedItem ?? throw new NotImplementedException();
			}

			throw new HttpRequestException($"The HTTP request failed with status code: {httpResponse.StatusCode}");
		}
		public async Task<Lottery> GetLottery(Guid lotteryId, CancellationToken cancellationToken = default)
		{
			cancellationToken.ThrowIfCancellationRequested();

			var httpResponse = await _httpClient.GetAsync($"{LotteryControllerName}/GetLottery/{lotteryId}", cancellationToken);

			httpResponse.EnsureSuccessStatusCode();
			if (httpResponse.IsSuccessStatusCode)
			{
				var result = await httpResponse.Content.ReadFromJsonAsync<Lottery>(cancellationToken);
				return result ?? throw new NotImplementedException();
			}

			throw new HttpRequestException($"The HTTP request failed with status code: {httpResponse.StatusCode}");
		}
		public async Task<bool> DeleteLotteryById(Guid lotteryID, CancellationToken cancellationToken = default)
		{
			cancellationToken.ThrowIfCancellationRequested();

			var httpResponse = await _httpClient.DeleteAsync($"{LotteryControllerName}/DeleteLottery/{lotteryID}", cancellationToken);
			httpResponse.EnsureSuccessStatusCode();

			if (!httpResponse.IsSuccessStatusCode)
			{
				throw new NotImplementedException();
			}
			return true;
		}

		public async Task<IEnumerable<Lottery>> GetAllLotteries(CancellationToken cancellationToken = default)
		{
			cancellationToken.ThrowIfCancellationRequested();

			var httpResponse = await _httpClient.GetAsync($"{LotteryControllerName}/GetAllLotteries/", cancellationToken);
			httpResponse.EnsureSuccessStatusCode();
			if (httpResponse.IsSuccessStatusCode)
			{
				var result = await httpResponse.Content.ReadFromJsonAsync<IList<Lottery>>(cancellationToken);
				return result ?? new List<Lottery>();
			}

			throw new NotImplementedException();
		}

		public async Task<PagedList<Lottery>> GetPaged(PagingParameters pagingParameters, CancellationToken cancellationToken = default)
		{
			cancellationToken.ThrowIfCancellationRequested();

			var httpResponse = await _httpClient.GetAsync($"{LotteryControllerName}/GetAllLotteriesPaged?pageNumber={pagingParameters.PageNumber}&pageSize={pagingParameters.PageSize}", cancellationToken);

			httpResponse.EnsureSuccessStatusCode();

			if (httpResponse.IsSuccessStatusCode)
			{

				var result = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
				var result2 = await httpResponse.Content.ReadFromJsonAsync<PagedList<Lottery>>(cancellationToken);

				return result2 ?? throw new NotImplementedException();
			}

			throw new HttpRequestException($"The HTTP request failed with status code: {httpResponse.StatusCode}");
		}
	}
}
