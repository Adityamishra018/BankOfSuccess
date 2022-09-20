using System;
using System.Collections.Generic;
using BankOfSuccess.Data.Entities;

namespace BankOfSuccess.Business
{
    public abstract class Transaction
    {
        public int Acc { get; set; }
        public float Amt { get; set; }
        public DateTime Date { get; set; }
    }

    public class WithDraw : Transaction { }
    public class Deposit : Transaction { }
    public class Transfer : Transaction 
    {
        public int To { get; set; }
    }

    public enum TRANSACTIONTYPE {
        WITHDRAW,DEPOSIT,TRANSFER
    }
    public interface ILogTransactions
    {
        void CreateLog(int acc);
        void UpdateLog(int from, float amt,TRANSACTIONTYPE t,int to=0);
        List<Transaction> GetLogs(int accNo,TRANSACTIONTYPE t);
    }

   
}
