
namespace LotteryProject.Models.Exceptions
{

    public class LotteryNotFoundException : BaseException
    {
        private const string RESOURCE_CODE = "LNF";

        public LotteryNotFoundException()
        {
            Code = RESOURCE_CODE;
            StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
        }

        public LotteryNotFoundException(string message) : base(message)
        {
            Code = RESOURCE_CODE;
            StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
        }

        public LotteryNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
            Code = RESOURCE_CODE;
            StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
        }
    }
}