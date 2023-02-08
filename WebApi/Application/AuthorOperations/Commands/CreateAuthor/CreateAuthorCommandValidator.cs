using FluentValidation;
using WebApi.Application.GenreOperations.Commands.CreateGenre;

namespace WebApi.Application.AuthorOperations.Commands.CreateAuthor
{
	public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
	{
		public CreateAuthorCommandValidator()
		{
			RuleFor(command => command.Model.Name).NotEmpty().MinimumLength(4);
		}
	}
}
