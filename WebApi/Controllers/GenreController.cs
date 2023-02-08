using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using FluentValidation;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.Application.GenreOperations.Queries.GetGenreDetails;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;

namespace WebApi.Controllers
{
	[ApiController]
	[Route("[controller]s")]
	public class GenreController : ControllerBase
	{
		private readonly BookStoreDbContext _context;
		private readonly IMapper _mapper;

		public GenreController(BookStoreDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		// Get all genres
		[HttpGet]
		public ActionResult GetGenres()
		{
			GetGenresQuery query = new GetGenresQuery(_context, _mapper);

			var obj = query.Handle();

			return Ok(obj);
		}
		[HttpGet("id")]
		public ActionResult GetGenreDetail(int id)
		{
			GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);
			query.GenreId = id;
			GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
			validator.ValidateAndThrow(query);

			var obj = query.Handle();
			return Ok(obj);
		}

		// adding genre
		[HttpPost]
		public IActionResult AddGenre([FromBody] CreateGenreModel newGenre)
		{
			CreateGenreCommand command = new CreateGenreCommand(_context);
			command.Model = newGenre;

			CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
			validator.ValidateAndThrow(command);
			command.Handle();

			return Ok(command);
		}

		//updating genre
		[HttpPut("id")]
		public IActionResult UpdadateGenre(int id, [FromBody] UpdateGenreModel updateGenre)
		{
			UpdateGenreCommand command = new UpdateGenreCommand(_context);
			command.GenreId = id;
			command.Model = updateGenre;

			UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
			validator.ValidateAndThrow(command);
			command.Handle();
			return Ok(command);
		}

		//deleting genre
		[HttpDelete("id")]
		public IActionResult DeleteGenre(int id)
		{
			DeleteGenreCommand command = new DeleteGenreCommand(_context);
			command.GenreId = id;

			DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
			validator.ValidateAndThrow(command);
			command.Handle();

			return Ok(command);
		}
	}
}
