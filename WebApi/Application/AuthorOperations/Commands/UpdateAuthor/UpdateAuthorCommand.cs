using Microsoft.EntityFrameworkCore;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.DBOperations;
using WebApi.Entitites;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
	public class UpdateAuthorCommand
	{
		public int AuthorId { get; set; }
		public UpdateAuthorModel Model { get; set; }

		private readonly BookStoreDbContext _context;

		public UpdateAuthorCommand(BookStoreDbContext context)
		{
			_context = context;
		}
		public void Handle()
		{
			var author = _context.Authors.SingleOrDefault(x => x.Id == AuthorId);

			// checking genre
			if (author == null)
			{
				throw new InvalidOperationException("Author couldn't found");
			}

			// checking existance
			if (_context.Authors.Any(x => x.Name.ToLower() == author.Name.ToLower() && x.Id != AuthorId))
			{
				throw new InvalidOperationException("There is a same Author name exists in database");
			}

			// Updating name without getting from user
			author.Name = string.IsNullOrEmpty(Model.Name.Trim()) ? author.Name : Model.Name;

			//Updating BirthDay
			author.BirthDay = Model.BirthDay;

			//saving changes
			_context.SaveChanges();
		}
	}
	public class UpdateAuthorModel
	{
		public string Name { get; set; }
		public string BirthDay { get; set; }

	}
}
