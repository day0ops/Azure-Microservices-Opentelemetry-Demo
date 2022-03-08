using Catalog.DB.Entity;

namespace Catalog.Repository.Interfaces
{
	public interface IBookRepository
	{
		Task<IEnumerable<BookEntity>> GetAllBooks();
		Task<bool> InsertBook(BookEntity entity);
	}
}