using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.AuthorOperations.Queries.GetAuthor;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.DBOperations;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using FluentValidation.Results;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetails;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;

namespace WebApi.Controllers
{
	[ApiController]
	[Route("[controller]s")]
	public class AuthorController : ControllerBase
	{
		private readonly BookStoreDbContext _context;
		private readonly IMapper _mapper;

		public AuthorController(BookStoreDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}
		[HttpGet]
		public ActionResult GetAuthors()
		{
			GetAuthorsQuery query = new GetAuthorsQuery(_context, _mapper);

			var obj = query.Handle();

			return Ok(obj);
		}
		[HttpGet("id")]
		public IActionResult GetAuthorDetail(int id)
		{
			GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context, _mapper);
			query.AuthorId = id;
			GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
			validator.ValidateAndThrow(query);

			var obj = query.Handle();
			return Ok(obj);
		}
		[HttpPost]
		public IActionResult AddAuthor([FromBody] CreateAuthorModel newAuthor)
		{
			CreateAuthorCommand command = new CreateAuthorCommand(_context);
			command.Model = newAuthor;

			CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
			validator.ValidateAndThrow(command);
			command.Handle();
			return Ok(command);
		}
		[HttpPut("id")]
		public IActionResult UpdateAuthor(int id, [FromBody] UpdateAuthorModel updateAuthor)
		{
			UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
			command.AuthorId = id;
			command.Model = updateAuthor;

			UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
			validator.ValidateAndThrow(command);
			command.Handle();
			return Ok(command);
		}
		[HttpDelete("id")]
		public IActionResult DeleteAuthor(int id)
		{
			DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
			command.AuthorId = id;

			DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
			validator.ValidateAndThrow(command);
			command.Handle();

			return Ok(command);
		}
	}
}
