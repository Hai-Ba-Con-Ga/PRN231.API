
using System.Net;

namespace Service.Exceptions
{
    public class CustomException : Exception
    {
        public string? ErrorMessages { get; }

        public HttpStatusCode StatusCode { get; }

        public CustomException(string message, string? errors, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
            : base(message)
        {
            ErrorMessages = errors;
            StatusCode = statusCode;
        }
    }
}