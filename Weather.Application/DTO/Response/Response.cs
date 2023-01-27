using System.Net;

namespace Weather.Application.DTO.Response
{
	public class Response<T>
	{
		public T Data { get; set; }
		public string Message { get; set; }
		public HttpStatusCode ResponseCode { get; set; }
		public bool Status { get; set; }


		public static Response<T> Fail(string errorMessage, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
		{
			return new Response<T> { Status = false, Message = errorMessage, ResponseCode = statusCode };
		}
		public static Response<T> Fail(string errorMessage, T data, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
		{
			return new Response<T> { Status = false, Message = errorMessage, Data = data, ResponseCode = statusCode };
		}
		public static Response<T> Success(string successMessage, T data, HttpStatusCode statusCode = HttpStatusCode.OK)
		{
			return new Response<T> { Status = true, Message = successMessage, Data = data, ResponseCode = statusCode };
		}
	}
}