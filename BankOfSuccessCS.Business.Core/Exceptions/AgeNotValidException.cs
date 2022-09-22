using System;

namespace BankOfSuccessCS.Business.Core
{
    public class AgeNotValidException : ApplicationException
    {
        public AgeNotValidException() : base("Account cannot be created, age not valid") { }
    }
}
