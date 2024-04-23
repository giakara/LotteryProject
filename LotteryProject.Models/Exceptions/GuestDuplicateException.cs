
namespace LotteryProject.Models.Exceptions
{

    public class GuestDuplicateException : BaseException
    {
        private const string RESOURCE_CODE = "GDNF";

        public GuestDuplicateException()
        {
            Code = RESOURCE_CODE;
            StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
        }

        public GuestDuplicateException(string message) : base(message)
        {
            Code = RESOURCE_CODE;
            StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
        }

        public GuestDuplicateException(string? message, Exception? innerException) : base(message, innerException)
        {
            Code = RESOURCE_CODE;
            StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
        }
    }
}
