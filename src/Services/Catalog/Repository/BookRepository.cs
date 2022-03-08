using Catalog.DB;
using Catalog.DB.Entity;
using Catalog.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Repository
{
	public class BookRepository : IBookRepository
	{
		private readonly BookDbContext _context;

		public BookRepository(BookDbContext dbContext)
		{
			_context = dbContext;
		}

		public async Task<IEnumerable<BookEntity>> GetAllBooks()
		{
			return await _context.Set<BookEntity>().AsNoTracking().ToListAsync();
		}

		public async Task<bool> InsertBook(BookEntity entity)
		{
			try
			{
				await _context.Set<BookEntity>().AddAsync(entity);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
    }
}