using WebApi.DBOperations;
using WebApi.Entitites;

namespace WebApi.Application.GenreOperations.Commands.CreateGenre
{
	public class CreateGenreCommand
	{
		public CreateGenreModel Model { get; set; }
		private readonly BookStoreDbContext _context;
		public CreateGenreCommand(BookStoreDbContext context)
		{
			_context = context;
		}
		public void Handle()
		{
			var genre = _context.Genres.SingleOrDefault(x => x.Name == Model.Name);
			if (genre != null)
			{
				throw new InvalidOperationException("Database already has this book type");
			}
			
			genre = new Genre();
			genre.Name = Model.Name;

			_context.Genres.Add(genre);
			_context.SaveChanges();
		}
	}

	public class CreateGenreModel
	{
		public string Name { get; set; }

	}
}
