using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;
using static WebApi.Application.BookOperations.Commands.CreateCommand.CreateBookCommand;
using static WebApi.Application.BookOperations.Commands.UpdateCommand.UpdateBookCommand;
using FluentValidation;
using WebApi.Application.BookOperations.Commands.CreateCommand;
using WebApi.Application.BookOperations.Commands.DeleteComand;
using WebApi.Application.BookOperations.Commands.UpdateCommand;
using WebApi.Application.BookOperations.Commands.Query;

namespace WebApi.Controllers
{
    [ApiController]
	[Route("[controller]s")]
	public class BookController : ControllerBase
	{
		private readonly BookStoreDbContext _context;
		private readonly IMapper _mapper;

		public BookController(BookStoreDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}


		[HttpGet]
		public IActionResult GetBooks()
		{
			GetBooksQuery query = new GetBooksQuery(_context, _mapper);
			var result = query.Handle();
			return Ok(result);

		}

		// get by id
		[HttpGet("{id}")]

		public IActionResult GetById(int id)
		{
			BookDetailViewModel result;

			GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
			query.BookId = id;
			GetBooksQueryValidator validator = new GetBooksQueryValidator();
			validator.ValidateAndThrow(query);
			result = query.Handle();


			return Ok(result);

		}

		// add book 
		[HttpPost]
		public IActionResult AddBook([FromBody] CreateBookModel newBook)
		{
			CreateBookCommand command = new CreateBookCommand(_context, _mapper);

			command.Model = newBook;
			CreateBookCommandValidator validator = new CreateBookCommandValidator();

			validator.ValidateAndThrow(command);
			command.Handle();
			return Ok();


		}

		// update book
		[HttpPut("{id}")]
		public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
		{

			UpdateBookCommand command = new UpdateBookCommand(_context);
			command.BookId = id;
			command.Model = updatedBook;
			UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
			validator.ValidateAndThrow(command);
			
			command.Handle();

			return Ok();
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteBook(int id)
		{

			DeleteBookCommand command = new DeleteBookCommand(_context);
			command.BookId = id;
			DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
			validator.ValidateAndThrow(command);
			command.Handle();


			return Ok();

		}
	}
}
