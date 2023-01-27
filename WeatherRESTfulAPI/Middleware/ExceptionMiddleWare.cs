using Newtonsoft.Json;
using System.Net;
using Weather.Application.DTO.Response;

namespace WeatherAPI.Middleware
{
	public class ExceptionMiddleware
	{
		private readonly RequestDelegate _next;
		//private readonly ILogger _logger;
		public ExceptionMiddleware(RequestDelegate next)
		{

			_next = next;
		}
		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception error)
			{
				var response = context.Response;
				response.ContentType = "application/json";
				var responseModel = Response<string>.Fail(error.Message, HttpStatusCode.InternalServerError);
				switch (error)
				{
					case UnauthorizedAccessException e:
						response.StatusCode = (int)HttpStatusCode.Unauthorized;
						responseModel.Message = e.Message;
						break;
					case ArgumentOutOfRangeException e:
						response.StatusCode = (int)HttpStatusCode.BadRequest;
						responseModel.Message = e.Message;
						break;
					case ArgumentNullException e:
						response.StatusCode = (int)HttpStatusCode.BadRequest;
						responseModel.Message = e.Message;
						break;
					case NullReferenceException e:
						response.StatusCode = (int)HttpStatusCode.BadRequest;
						responseModel.Message = e.Message;
						break;
					default:
						response.StatusCode = (int)HttpStatusCode.InternalServerError;
						responseModel.Message = "Internal Server Error. Please Try Again Later.";
						break;
				}
				var result = JsonConvert.SerializeObject(responseModel);
				await response.WriteAsync(result);
			}
		}
	}
}