using LibraryPortal.Models;

namespace LibraryPortal.Services.Interfaces
{
	public interface IBookService
	{
		Task<(bool IsSuccess, IEnumerable<Book> ObjBooks)> GetBooks();
	}
}