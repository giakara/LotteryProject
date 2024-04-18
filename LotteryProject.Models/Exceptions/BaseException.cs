

namespace LotteryProject.Models.Exceptions
{
    public class BaseException : Exception
    {
        public string Code { get; protected init; }
        public int StatusCode { get; protected init; }

        public BaseException()
        {
            Code = string.Empty;
            StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
        }
        public BaseException(string message) : base(message)
        {
            Code = string.Empty;
            StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
        }

        public BaseException(string? message, Exception? innerException) : base(message, innerException)
        {
            Code = string.Empty;
            StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
        }
    }
}
