using System;

namespace BankOfSuccessCS.Business.Core
{
    public class RegistrationIsNullExcepiton : ApplicationException
    {
        public RegistrationIsNullExcepiton() : base("Registration Number Should Not Be NULL") { }
    }
}
