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

namespace LotteryProject.Client.Shared.Services
{
	public class GuestService : IGuestService
	{
		private readonly HttpClient _httpClient;
		private const string GuestControllerName = "https://localhost:7292/api/Guest";
		private readonly JsonSerializerOptions _jsonSerializerOptions;
		public GuestService(HttpClient httpClient)
		{
			_httpClient = httpClient;
			_jsonSerializerOptions = new JsonSerializerOptions()
			{
				PropertyNameCaseInsensitive = true
			};

		}
		public async Task<Guest> AddGuest(AddGuestDTO guest, CancellationToken cancellationToken = default)
		{
			cancellationToken.ThrowIfCancellationRequested();

			var httpResponse = await _httpClient.PostAsJsonAsync(
									requestUri: $"{GuestControllerName}/AddGuest/",
									value: guest,
									cancellationToken: cancellationToken);

			if (httpResponse.IsSuccessStatusCode)
			{
				var responseBody = await httpResponse.Content.ReadAsStreamAsync();
				var jsonSerializerOptions = new JsonSerializerOptions()
				{
					PropertyNameCaseInsensitive = true,
				};
				var addedItem = await JsonSerializer.DeserializeAsync<Guest>(responseBody, jsonSerializerOptions);

				return addedItem ?? throw new NotImplementedException();
			}

			throw new HttpRequestException($"The HTTP request failed with status code: {httpResponse.StatusCode}");
		}

		public async Task<bool> DeleteGuestById(Guid guestID, CancellationToken cancellationToken = default)
		{
			cancellationToken.ThrowIfCancellationRequested();

			var httpResponse = await _httpClient.DeleteAsync($"{GuestControllerName}/DeleteGuest/{guestID}", cancellationToken);
			httpResponse.EnsureSuccessStatusCode();

			if (!httpResponse.IsSuccessStatusCode)
			{
				throw new NotImplementedException();
			}
			return true;
		}

		public async Task<IEnumerable<Guest>> GetAllGuests(CancellationToken cancellationToken = default)
		{
			cancellationToken.ThrowIfCancellationRequested();

			var httpResponse = await _httpClient.GetAsync($"{GuestControllerName}/GetAllGuests/", cancellationToken);
			httpResponse.EnsureSuccessStatusCode();
			if (httpResponse.IsSuccessStatusCode)
			{
				var result = await httpResponse.Content.ReadFromJsonAsync<IList<Guest>>(cancellationToken);
				return result ?? new List<Guest>();
			}

			throw new NotImplementedException();
		}

		public async Task<Guest> GetGuest(Guid guestId, CancellationToken cancellationToken = default)
		{
			cancellationToken.ThrowIfCancellationRequested();

			var httpResponse = await _httpClient.GetAsync($"{GuestControllerName}/GetGuest/{guestId}", cancellationToken);

			httpResponse.EnsureSuccessStatusCode();
			if (httpResponse.IsSuccessStatusCode)
			{
				var result = await httpResponse.Content.ReadFromJsonAsync<Guest>(cancellationToken);
				return result ?? throw new NotImplementedException();
			}

			throw new HttpRequestException($"The HTTP request failed with status code: {httpResponse.StatusCode}");
		}

		public async Task<Guest> UpdateGuest(Guest guest, CancellationToken cancellationToken = default)
		{
			cancellationToken.ThrowIfCancellationRequested();

			try
			{
				//var serializedItem = new StringContent(JsonSerializer.Serialize(requestItem), Encoding.UTF8, "application/json");

				var httpResponse = await _httpClient.PutAsJsonAsync(
										requestUri: $"{GuestControllerName}/UpdateGuest/",
										value: guest,
										cancellationToken: cancellationToken);

				if (httpResponse.IsSuccessStatusCode)
				{
					var responseBody = await httpResponse.Content.ReadAsStreamAsync();

					var responseItem = await JsonSerializer.DeserializeAsync<Guest>(responseBody);

					if (responseItem is null)
					{
						throw new NotImplementedException();
					}
					return responseItem;
				}

				throw new HttpRequestException($"The HTTP request failed with status code: {httpResponse.StatusCode}");
			}
			catch (Exception)
			{
				throw new NotImplementedException();
			}
		}
		public async Task<PagedList<Guest>> GetPaged(PagingParameters pagingParameters, CancellationToken cancellationToken = default)
		{
			cancellationToken.ThrowIfCancellationRequested();

			var httpResponse = await _httpClient.GetAsync($"{GuestControllerName}/GetAllGuestsPaged?pageNumber={pagingParameters.PageNumber}&pageSize={pagingParameters.PageSize}", cancellationToken);

			httpResponse.EnsureSuccessStatusCode();

			if (httpResponse.IsSuccessStatusCode)
			{

				var result = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
				var result2 = await httpResponse.Content.ReadFromJsonAsync<PagedList<Guest>>(cancellationToken);

				return result2 ?? throw new NotImplementedException();
			}

			throw new HttpRequestException($"The HTTP request failed with status code: {httpResponse.StatusCode}");
		}
	}
}
