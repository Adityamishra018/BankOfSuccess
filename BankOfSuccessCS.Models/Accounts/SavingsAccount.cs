using System;

namespace BankOfSuccessCS.Models
{
    public class SavingsAccount : Account
    {
        public string Gender { get; set; }
        public DateTime DOB { get; set; }
        public SavingsAccount(int accNo, string name, int pin,string mail,string ph) : base(accNo, name, pin,mail,ph) { }
    }
}
