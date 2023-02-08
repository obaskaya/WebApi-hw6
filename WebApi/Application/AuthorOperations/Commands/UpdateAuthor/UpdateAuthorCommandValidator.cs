using FluentValidation;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
	public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
	{
		public UpdateAuthorCommandValidator()
		{
			RuleFor(command => command.Model.Name).MinimumLength(4).When(x => x.Model.Name != string.Empty);
		}
	}
}
