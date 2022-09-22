using System;

namespace BankOfSuccessCS.Business.Core
{
    public class IncorrectPinException : ApplicationException
    {
        public IncorrectPinException() : base("Incorrect Pin Number") { }
    }
}
