using System;

namespace BankOfSuccess.Data.Entities
{
    public class SavingsAccount : Account
    {
        public string Gender { get; set; }
        public string PhoneNo { get; set; }
        public DateTime DOB { get; set; }
        public SavingsAccount(int accNo, string name, int pin, string gender, string phoneNo, DateTime dob, float bal = 500) : base(accNo, name, pin, bal, DateTime.Now)
        {
            Gender = gender;
            PhoneNo = phoneNo;
            DOB = dob;
        }
    }
}
