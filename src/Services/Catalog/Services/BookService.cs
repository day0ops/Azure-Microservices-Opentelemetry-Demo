using AutoMapper;
using Catalog.DB.Entity;
using Catalog.Models;
using Catalog.Repository.Interfaces;
using Catalog.Services.Interfaces;

namespace Catalog.Services
{
	public class BookService : IBookService
	{
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

            Task.Run(() => CreateBookInMemory()).Wait();
        }

        public async Task<(bool IsSuccess, IEnumerable<BookModel> Books, string ErrorMessage)> GetBooksAsync()
        {
            try
            {
                var booksList = await _unitOfWork.Books.GetAllBooks();
                if (booksList != null && booksList.Any())
                {
                    return (true, _mapper.Map<IEnumerable<BookModel>>(booksList), null);
                }
                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                return (false, null, ex.Message);
            }
        }

        private async Task CreateBookInMemory()
        {
            var booksList = await _unitOfWork.Books.GetAllBooks();
            if (!booksList.Any())
            {
                var book1 = new BookEntity { Id = 1, Title = "Hamlet", Category = "Drama" };
                var book2 = new BookEntity { Id = 2, Title = "To Kill a Mockingbird", Category = "Classics" };
                var book3 = new BookEntity { Id = 3, Title = "The Exorcist", Category = "Horror" };
                var book4 = new BookEntity { Id = 4, Title = "The Shining", Category = "Horror" };
                var book5 = new BookEntity { Id = 5, Title = "The Da Vinci Code", Category = "Mystery" };

                await _unitOfWork.Books.InsertBook(book1);
                await _unitOfWork.Books.InsertBook(book2);
                await _unitOfWork.Books.InsertBook(book3);
                await _unitOfWork.Books.InsertBook(book4);
                await _unitOfWork.Books.InsertBook(book5);

                await _unitOfWork.Save();
            }
        }
    }
}