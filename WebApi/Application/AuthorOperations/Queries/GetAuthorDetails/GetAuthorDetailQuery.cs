using AutoMapper;
using System.Linq;
using WebApi.Application.GenreOperations.Queries.GetGenreDetails;
using WebApi.DBOperations;
using WebApi.Entitites;
using static WebApi.Application.AuthorOperations.Queries.GetAuthor.GetAuthorsQuery;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthorDetails
{
	public class GetAuthorDetailQuery
	{
		public int AuthorId { get; set; }
		public readonly BookStoreDbContext _context;
		public readonly IMapper _mapper;
		public GetAuthorDetailQuery(BookStoreDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;

		}
		public AuthorDetailViewModel Handle()
		{
			var author = _context.Authors.Where(author => author.Id == AuthorId).SingleOrDefault();
			if (author == null)
			{
				throw new InvalidOperationException("Author couldn't found");
			}

			AuthorDetailViewModel returnObj = _mapper.Map<AuthorDetailViewModel>(author);
			return returnObj;

		}
	}
	public class AuthorDetailViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string BirthDate { get; set; }
	}
}
