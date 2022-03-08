namespace Catalog.Repository.Interfaces
{
	public interface IUnitOfWork : IDisposable
	{
		IBookRepository Books { get; }
		Task Save();
	}
}