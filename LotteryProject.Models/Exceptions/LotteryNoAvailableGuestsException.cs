
namespace LotteryProject.Models.Exceptions
{

    public class LotteryNoAvailableGuestsException : BaseException
    {
        private const string RESOURCE_CODE = "LNAGNF";

        public LotteryNoAvailableGuestsException()
        {
            Code = RESOURCE_CODE;
            StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
        }

        public LotteryNoAvailableGuestsException(string message) : base(message)
        {
            Code = RESOURCE_CODE;
            StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
        }

        public LotteryNoAvailableGuestsException(string? message, Exception? innerException) : base(message, innerException)
        {
            Code = RESOURCE_CODE;
            StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
        }
    }
}