using WebApi.DBOperations;

namespace WebApi.Application.GenreOperations.Commands.UpdateGenre
{
	public class UpdateGenreCommand
	{
		public int GenreId { get; set; }
		public UpdateGenreModel Model { get; set; }

		private readonly BookStoreDbContext _context;

		public UpdateGenreCommand(BookStoreDbContext context)
		{
			_context = context;
		}
		public void Handle()
		{

			var genre = _context.Genres.SingleOrDefault(x => x.Id == GenreId);

			// checking genre
			if (genre == null)
			{
				throw new InvalidOperationException("Book type couldn't found");
			}

			// checking existance
			if (_context.Genres.Any(x => x.Name.ToLower() == genre.Name.ToLower() && x.Id != GenreId))
			{
				throw new InvalidOperationException("There is a same book type name exists in database");
			}

			// Updating name without getting from user
			genre.Name = string.IsNullOrEmpty(Model.Name.Trim()) ? genre.Name : Model.Name;

			//Updating is active 
			genre.IsActive = Model.IsActive;

			//saving changes
			_context.SaveChanges();
		}
	}
	public class UpdateGenreModel
	{
		public string Name { get; set; }
		public bool IsActive { get; set; } = true;

	}
}
