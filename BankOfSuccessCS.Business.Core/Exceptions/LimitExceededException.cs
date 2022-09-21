using System;

namespace BankOfSuccessCS.Business.Core
{
    public class LimitExceededException : ApplicationException
    {
        public LimitExceededException() : base("Daily Limit Exceeded") { }
    }
}
