
namespace LotteryProject.Models.Exceptions
{

    public class GuestNotFoundException : BaseException
    {
        private const string RESOURCE_CODE = "GNF";

        public GuestNotFoundException()
        {
            Code = RESOURCE_CODE;
            StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
        }

        public GuestNotFoundException(string message) : base(message)
        {
            Code = RESOURCE_CODE;
            StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
        }

        public GuestNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
            Code = RESOURCE_CODE;
            StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
        }
    }
}
