using Catalog.DB;
using Catalog.Repository.Interfaces;

namespace Catalog.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly BookDbContext _context;

		public UnitOfWork(BookDbContext context)
		{
			_context = context;
			Books = new BookRepository(_context);
		}

		public IBookRepository Books { get; private set; }

		public async Task Save()
		{
			await _context.SaveChangesAsync();
		}

		public void Dispose()
		{
			_context.Dispose();
		}
	}
}