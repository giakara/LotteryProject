
namespace LotteryProject.Models.Exceptions
{

    public class LotteryPresentIsGiftedException : BaseException
    {
        private const string RESOURCE_CODE = "LPIGNF";

        public LotteryPresentIsGiftedException()
        {
            Code = RESOURCE_CODE;
            StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
        }

        public LotteryPresentIsGiftedException(string message) : base(message)
        {
            Code = RESOURCE_CODE;
            StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
        }

        public LotteryPresentIsGiftedException(string? message, Exception? innerException) : base(message, innerException)
        {
            Code = RESOURCE_CODE;
            StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
        }
    }
}
