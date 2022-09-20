using System;

namespace BankOfSuccess.Data.Entities
{
    public class CurrentAccount : Account
    {
        public string CompanyName { get; set; }

        public string Website { get; set; }

        public string RegistrationNo { get; set; }
        public CurrentAccount(int accNo, string name, int pin, string companyName, string website, string registrationNo, float bal = 500) : base(accNo, name, pin, bal, DateTime.Now)
        {
            CompanyName = companyName;
            Website = website;
            RegistrationNo = registrationNo;
        }
    }
}
