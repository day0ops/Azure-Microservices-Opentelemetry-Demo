using Catalog.Models;

namespace Catalog.Services.Interfaces
{
	public interface IBookService
	{
		Task<(bool IsSuccess, IEnumerable<BookModel> Books, string ErrorMessage)> GetBooksAsync();
	}
}