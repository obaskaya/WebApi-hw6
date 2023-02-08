using AutoMapper;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthor
{
	public class GetAuthorsQuery
	{
		public readonly BookStoreDbContext _context;
		public readonly IMapper _mapper;
		public GetAuthorsQuery(BookStoreDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}
		public List<AuthorsViewModel> Handle()
		{
			var authors = _context.Authors.OrderBy(x => x.Id).ToList();
			List<AuthorsViewModel> returnObj = _mapper.Map<List<AuthorsViewModel>>(authors);
			return returnObj;
		}
		public class AuthorsViewModel
		{
			public int Id { get; set; }
			public string Name { get; set; }
			public string BirthDay { get; set; }
		}

	}
}
