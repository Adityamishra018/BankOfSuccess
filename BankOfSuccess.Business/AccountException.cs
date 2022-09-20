using System;

namespace BankOfSuccess.Business
{
    public class AccountException : ApplicationException
    {
        public AccountException(string message) : base(message)
        {
        }
    }
}
