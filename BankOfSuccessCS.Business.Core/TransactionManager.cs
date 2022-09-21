using BankOfSuccessCS.Models;
using System.Collections.Generic;

namespace BankOfSuccessCS.Business.Core
{
    public class TransactionManager : ITransactionManager
    {
        private Dictionary<int, Dictionary<TransactionType, List<Transaction>>> transactionLog = new Dictionary<int, Dictionary<TransactionType, List<Transaction>>>();

        public void Create(int AccNo)
        {
            var dict = new Dictionary<TransactionType, List<Transaction>>();
            dict.Add(TransactionType.TRANSFER, new List<Transaction>());
            dict.Add(TransactionType.DEPOSIT, new List<Transaction>());
            dict.Add(TransactionType.WITHDRAWAL, new List<Transaction>());
            transactionLog.Add(AccNo, dict);
        }
        public void Add(int AccNo, TransactionType transactionType, Transaction t)
        {
            transactionLog[AccNo][transactionType].Add(t);
        }

        public List<Transaction> Get(int accNo, TransactionType t)
        {
            return transactionLog[accNo][t];
        }
    }
}
