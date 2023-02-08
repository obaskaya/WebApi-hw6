using FluentValidation;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthorDetails
{
	public class GetAuthorDetailQueryValidator : AbstractValidator<GetAuthorDetailQuery>
	{
		public GetAuthorDetailQueryValidator()
		{
			RuleFor(query => query.AuthorId).GreaterThan(0);
		}
	}
}
