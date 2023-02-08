using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.Reflection.Metadata;
using System.Xml;
using WebApi.Services;

namespace CarRentWebApi.Middlewares
{
	public class CustomExceptionMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILoggerService _loggerService;
		public CustomExceptionMiddleware(RequestDelegate next, ILoggerService loggerService)
		{
			_next = next;
			_loggerService = loggerService;
		}

		public async Task Invoke(HttpContext context)
		{
			var watch = Stopwatch.StartNew();
			try
			{

				string message = "[Request] HTTP " + context.Request.Method + " " + context.Request.Path;
				_loggerService.Write(message);
				await _next(context);

				message = "[Response] HTTP " + context.Request.Method + " - " + context.Request.Path + " responded " + context.Response.StatusCode + " in" + watch.Elapsed.TotalMilliseconds + "ms";
				_loggerService.Write(message);
			}
			catch (Exception ex)
			{
				watch.Stop();
				await HandleException(context, ex, watch);
			}

		}
		private  Task HandleException(HttpContext context, Exception ex, Stopwatch watch)
		{
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

			string message = "[Error] HTTP" + context.Request.Method + " - error " + context.Response.StatusCode + " Error Message " + ex.Message + " in " + watch.Elapsed.TotalMilliseconds + " ms";
			_loggerService.Write(message);

			string json = JsonConvert.SerializeObject(message);
			return context.Response.WriteAsync(json);

		}


	}
	public static class CustomExceptionMiddlewareExtension
	{
		public static IApplicationBuilder UseCustomExceptionMiddle(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<CustomExceptionMiddleware>();
		}
	}
}
