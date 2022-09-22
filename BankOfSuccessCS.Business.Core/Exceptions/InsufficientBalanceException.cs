using System;

namespace BankOfSuccessCS.Business.Core
{
    public class InsufficientBalanceException : ApplicationException
    {
        public InsufficientBalanceException() : base("Insufficient Balance to Withdraw Amount") { }
    }
}
