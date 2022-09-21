using System;

namespace BankOfSuccess.Data
{
    public enum TransactionType
    {
        Withdrawal,
        Deposit,
        Transfer
    }
    public abstract class Transaction
    {
        public int AccNo { get; set; }
        public float Amt { get; set; }
        public DateTime Date { get; set; }
    }


}
