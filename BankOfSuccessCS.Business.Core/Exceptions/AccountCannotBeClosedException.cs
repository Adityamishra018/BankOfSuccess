using System;

namespace BankOfSuccessCS.Business.Core
{
    public class AccountCannotBeClosedException : ApplicationException
    {
        public AccountCannotBeClosedException() : base("To Close an Account Balance Should Be Zero") { }
    }
}
