using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.DBOperations;
using WebApi.Entitites;

namespace WebApi.Application.AuthorOperations.Commands.CreateAuthor
{
	public class CreateAuthorCommand
	{
		public CreateAuthorModel Model { get; set; }
		private readonly BookStoreDbContext _context;
		public CreateAuthorCommand(BookStoreDbContext context)
		{
			_context = context;
		}
		public void Handle()
		{
			var author = _context.Authors.SingleOrDefault(x => x.Name == Model.Name);
			if (author != null)
			{
				throw new InvalidOperationException("Database already has this book type");
			}

			author = new Author();
			author.Name = Model.Name;
			author.BirthDay = Model.BirthDay;

			_context.Authors.Add(author);
			_context.SaveChanges();
		}
	}
	public class CreateAuthorModel
	{
		public string Name { get; set; }
		public string BirthDay { get; set; }

	}
}
