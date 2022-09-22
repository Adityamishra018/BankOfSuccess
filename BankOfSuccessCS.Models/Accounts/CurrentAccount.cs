using System;

namespace BankOfSuccessCS.Models
{
    public class CurrentAccount : Account
    {
        public string CompanyName { get; set; }

        public string Website { get; set; }

        public string RegistrationNo { get; set; }
        public CurrentAccount(int accNo, string name, int pin,string mail,string ph) : base(accNo, name, pin,mail,ph) { }
    }
}
