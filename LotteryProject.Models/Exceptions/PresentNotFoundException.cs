﻿

namespace LotteryProject.Models.Exceptions
{

    public class PresentNotFoundException : BaseException
    {
        private const string RESOURCE_CODE = "PRNF";

        public PresentNotFoundException()
        {
            Code = RESOURCE_CODE;
            StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
        }

        public PresentNotFoundException(string message) : base(message)
        {
            Code = RESOURCE_CODE;
            StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
        }

        public PresentNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
            Code = RESOURCE_CODE;
            StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
        }
    }
}
