using WebApi.DBOperations;
using WebApi.Entitites;

namespace WebApi.Application.AuthorOperations.Commands.DeleteAuthor
{
	public class DeleteAuthorCommand
	{
		public int AuthorId { get; set; }
		private readonly BookStoreDbContext _context;
		public DeleteAuthorCommand(BookStoreDbContext context)
		{
			_context = context;
		}
		public void Handle()
		{
			var author = _context.Authors.SingleOrDefault(x => x.Id == AuthorId);
			if (author == null)
			{
				throw new InvalidOperationException("Author Couldn't Found");
			}
			var c = _context.Books.Where(x => x.AuthorId == AuthorId).Count();
			if (c == 0)
			{
				_context.Authors.Remove(author);
				_context.SaveChanges();
			}
			else
			{
				throw new InvalidOperationException(("You can't delete this author. Because he/she have ") + c + (" books or book"));

			}
		}
	}
}
