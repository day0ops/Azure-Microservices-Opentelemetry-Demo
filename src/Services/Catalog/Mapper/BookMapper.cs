using AutoMapper;
using Catalog.DB.Entity;
using Catalog.Models;

namespace Catalog.Mapper
{
	public class BookMapper : Profile
	{
		public BookMapper()
		{
			CreateMap<BookEntity, BookModel>();
		}
	}
}