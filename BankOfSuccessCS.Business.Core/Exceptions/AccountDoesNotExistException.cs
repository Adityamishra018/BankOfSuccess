using System;

namespace BankOfSuccessCS.Business.Core
{
    public class AccountDoesNotExistException : ApplicationException
    {
        public AccountDoesNotExistException() : base("Account Doesnot Exist") { }
    }
}
