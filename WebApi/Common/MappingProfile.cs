using AutoMapper;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetails;
using WebApi.Application.BookOperations.Commands.Query;
using WebApi.Application.GenreOperations.Queries.GetGenreDetails;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.Entitites;
using static WebApi.Application.AuthorOperations.Queries.GetAuthor.GetAuthorsQuery;
using static WebApi.Application.BookOperations.Commands.CreateCommand.CreateBookCommand;

namespace WebApi.Common
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<CreateBookModel, Book>();
			CreateMap<Book, BookDetailViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));
			CreateMap<Book, BookDetailViewModel>().ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author.Name));
			CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));
			CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author.Name));
			CreateMap<Genre, GenresViewModel>();
			CreateMap<Genre, GenreDetailViewModel>();
			CreateMap<Author, AuthorsViewModel>();
			CreateMap<Author, AuthorDetailViewModel>();
		}
	}
}
