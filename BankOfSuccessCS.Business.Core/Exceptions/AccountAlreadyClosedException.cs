using System;

namespace BankOfSuccessCS.Business.Core
{
    public class AccountAlreadyClosedException : ApplicationException
    {
        public AccountAlreadyClosedException() : base("Account is already Closed") { }
    }
}
