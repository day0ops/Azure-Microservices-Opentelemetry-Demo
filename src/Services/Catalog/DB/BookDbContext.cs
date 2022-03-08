using Catalog.DB.Entity;
using Microsoft.EntityFrameworkCore;

namespace Catalog.DB
{
	public class BookDbContext : DbContext
	{
		public BookDbContext(DbContextOptions<BookDbContext> options) : base(options)
		{
		}

		public DbSet<BookEntity> Books { get; set; }
	}
}