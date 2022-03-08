using System.Text.Json;
using LibraryPortal.Models;
using LibraryPortal.Services.Interfaces;

namespace LibraryPortal.Services
{
	public class BookService : IBookService
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public BookService(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		public async Task<(bool IsSuccess, IEnumerable<Book> ObjBooks)> GetBooks()
		{
			try
			{
				var bookService = _httpClientFactory.CreateClient("BookService");
				var responseMicroserviceBook = await bookService.GetAsync($"api/v1/Books");

				if (responseMicroserviceBook.IsSuccessStatusCode)
				{
					var outputMicroserviceCustomer = await responseMicroserviceBook.Content.ReadAsByteArrayAsync();
					var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
					var outputBooks = JsonSerializer.Deserialize<IEnumerable<Book>>(outputMicroserviceCustomer, options);

					return (true, outputBooks);
				}
				return (false, null);
			}
			catch
			{
				return (false, null);
			}
		}
	}
}