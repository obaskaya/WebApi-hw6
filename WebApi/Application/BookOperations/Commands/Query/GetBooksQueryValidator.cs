using FluentValidation;

namespace WebApi.Application.BookOperations.Commands.Query
{
    public class GetBooksQueryValidator : AbstractValidator<GetBookDetailQuery>
    {
        public GetBooksQueryValidator()
        {
            RuleFor(query => query.BookId).GreaterThan(0);
        }
    }
}
