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
	public class PresentService : IPresentService
	{
		private readonly HttpClient _httpClient;
		private const string PresentControllerName = "https://localhost:7292/api/Present";
		private readonly JsonSerializerOptions _jsonSerializerOptions;
		public PresentService(HttpClient httpClient)
		{
			_httpClient = httpClient;
			_jsonSerializerOptions = new JsonSerializerOptions()
			{
				PropertyNameCaseInsensitive = true
			};

		}
		public async Task<Present> AddPresent(AddPresentDTO present, CancellationToken cancellationToken = default)
		{
			cancellationToken.ThrowIfCancellationRequested();

			var httpResponse = await _httpClient.PostAsJsonAsync(
									requestUri: $"{PresentControllerName}/AddPresent/",
									value: present,
									cancellationToken: cancellationToken);

			if (httpResponse.IsSuccessStatusCode)
			{
				var responseBody = await httpResponse.Content.ReadAsStreamAsync();
				var jsonSerializerOptions = new JsonSerializerOptions()
				{
					PropertyNameCaseInsensitive = true,
				};
				var addedItem = await JsonSerializer.DeserializeAsync<Present>(responseBody, jsonSerializerOptions);

				return addedItem ?? throw new NotImplementedException();
			}

			throw new HttpRequestException($"The HTTP request failed with status code: {httpResponse.StatusCode}");
		}

		public async Task<bool> DeletePresentById(Guid presentID, CancellationToken cancellationToken = default)
		{
			cancellationToken.ThrowIfCancellationRequested();

			var httpResponse = await _httpClient.DeleteAsync($"{PresentControllerName}/DeletePresent/{presentID}", cancellationToken);
			httpResponse.EnsureSuccessStatusCode();

			if (!httpResponse.IsSuccessStatusCode)
			{
				throw new NotImplementedException();
			}
			return true;
		}

		public async Task<IEnumerable<Present>> GetAllPresents(CancellationToken cancellationToken = default)
		{
			cancellationToken.ThrowIfCancellationRequested();

			var httpResponse = await _httpClient.GetAsync($"{PresentControllerName}/GetAllPresents/", cancellationToken);
			httpResponse.EnsureSuccessStatusCode();
			if (httpResponse.IsSuccessStatusCode)
			{
				var result = await httpResponse.Content.ReadFromJsonAsync<IList<Present>>(cancellationToken);
				return result ?? new List<Present>();
			}

			throw new NotImplementedException();
		}

		public async Task<Present> GetPresent(Guid presentId, CancellationToken cancellationToken = default)
		{
			cancellationToken.ThrowIfCancellationRequested();

			var httpResponse = await _httpClient.GetAsync($"{PresentControllerName}/GetPresent/{presentId}", cancellationToken);

			httpResponse.EnsureSuccessStatusCode();
			if (httpResponse.IsSuccessStatusCode)
			{
				var result = await httpResponse.Content.ReadFromJsonAsync<Present>(cancellationToken);
				return result ?? throw new NotImplementedException();
			}

			throw new HttpRequestException($"The HTTP request failed with status code: {httpResponse.StatusCode}");
		}

		public async Task<Present> UpdatePresent(Present present, CancellationToken cancellationToken = default)
		{
			cancellationToken.ThrowIfCancellationRequested();

			try
			{
				//var serializedItem = new StringContent(JsonSerializer.Serialize(requestItem), Encoding.UTF8, "application/json");

				var httpResponse = await _httpClient.PutAsJsonAsync(
										requestUri: $"{PresentControllerName} /GetPresent/{present.Id}",
										value: present,
										cancellationToken: cancellationToken);

				if (httpResponse.IsSuccessStatusCode)
				{
					var responseBody = await httpResponse.Content.ReadAsStreamAsync();

					var responseItem = await JsonSerializer.DeserializeAsync<Present>(responseBody);

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
		public async Task<PagedList<Present>> GetPaged(PagingParameters pagingParameters, CancellationToken cancellationToken = default)
		{
			cancellationToken.ThrowIfCancellationRequested();

			var httpResponse = await _httpClient.GetAsync($"{PresentControllerName}/GetAllPresentsPaged?pageNumber={pagingParameters.PageNumber}&pageSize={pagingParameters.PageSize}", cancellationToken);

			httpResponse.EnsureSuccessStatusCode();

			if (httpResponse.IsSuccessStatusCode)
			{

				var result = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
				var result2 = await httpResponse.Content.ReadFromJsonAsync<PagedList<Present>>(cancellationToken);

				return result2 ?? throw new NotImplementedException();
			}

			throw new HttpRequestException($"The HTTP request failed with status code: {httpResponse.StatusCode}");
		}
		public async Task<PagedList<Present>> SearchPresents(string searchText, PagingParameters pagingParameters, CancellationToken cancellationToken = default)
		{
			cancellationToken.ThrowIfCancellationRequested();

			var httpResponse = await _httpClient.GetAsync($"{PresentControllerName}/SearchPresent/{searchText}/?pageNumber={pagingParameters.PageNumber}&pageSize={pagingParameters.PageSize}", cancellationToken);

			httpResponse.EnsureSuccessStatusCode();

			if (httpResponse.IsSuccessStatusCode)
			{

				var result = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
				var result2 = await httpResponse.Content.ReadFromJsonAsync<PagedList<Present>>(cancellationToken);

				return result2 ?? throw new NotImplementedException();
			}
			throw new HttpRequestException($"The HTTP request failed with status code: {httpResponse.StatusCode}");

		}
	}
}
