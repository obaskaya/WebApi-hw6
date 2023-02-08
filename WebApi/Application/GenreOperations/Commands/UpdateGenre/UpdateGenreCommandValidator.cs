using FluentValidation;

namespace WebApi.Application.GenreOperations.Commands.UpdateGenre
{
	public class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
	{
		public UpdateGenreCommandValidator()
		{
			//if model name is not empty set min length 4

			RuleFor(command => command.Model.Name).MinimumLength(4).When(x => x.Model.Name != string.Empty);
		
		}

	}
}
