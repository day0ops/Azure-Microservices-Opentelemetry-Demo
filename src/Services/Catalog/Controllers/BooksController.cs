using System.Diagnostics;
using System.Text.Json;
using Catalog.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using OpenTelemetry;

namespace Catalog.Controllers
{
	[Route("api/v1/[controller]")]
	[ApiController]
	public class BooksController : ControllerBase
	{
		private readonly IBookService _bookService;
		private readonly ILogger<BooksController> _logger;

		public BooksController(IBookService bookService, ILogger<BooksController> logger)
		{
			_bookService = bookService;
			_logger = logger;
		}

		[HttpGet]
		public async Task<ActionResult> GetAllBooksAsync()
        {
			var baggage = Baggage.Current;

			var serializerSettings = new JsonSerializerOptions()
			{
				WriteIndented = true
			};
            _logger.LogInformation("-----------------> Logging Baggages In The Controller <-----------------");
            _logger.LogInformation($"Baggage: {JsonSerializer.Serialize(baggage.GetBaggage(), serializerSettings)}");


            var booksList = await _bookService.GetBooksAsync();
			if (booksList.IsSuccess)
			{
				return Ok(booksList.Books);
			}
			return NotFound(booksList.ErrorMessage);
		}
	}
}