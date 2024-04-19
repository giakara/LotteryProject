namespace LotteryProject.Models.Exceptions
{

    public class LotteryPresentIsRequiredException : BaseException
    {
        private const string RESOURCE_CODE = "LPrNF";

        public LotteryPresentIsRequiredException()
        {
            Code = RESOURCE_CODE;
            StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
        }

        public LotteryPresentIsRequiredException(string message) : base(message)
        {
            Code = RESOURCE_CODE;
            StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
        }

        public LotteryPresentIsRequiredException(string? message, Exception? innerException) : base(message, innerException)
        {
            Code = RESOURCE_CODE;
            StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
        }
    }
}
