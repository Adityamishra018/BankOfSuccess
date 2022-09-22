using System;

namespace BankOfSuccessCS.Models
{
    public class Transaction
    {
        public Account Acc { get; set; }
        public float Amt { get; set; }
        public DateTime Date { get; set; }
    }
}
