using LibraryPortal.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IBookService bookService, IConfiguration configuration, ILogger<OrdersController> logger)
        {
            _bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("books")]
        public async Task<ActionResult<string>> GetAllBooks()
        {
            var listOfBooks = await _bookService.GetBooks();
            if (listOfBooks.IsSuccess && listOfBooks.ObjBooks.Any())
            {
                return Ok();
            }
            return NotFound();
        }
    }
}