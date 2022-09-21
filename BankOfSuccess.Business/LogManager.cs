using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using BankOfSuccess.Data;

namespace BankOfSuccess.Business
{
    public class LogManager : ILogManager
    {
        private Dictionary<int, Dictionary<TransactionType, List<Transaction>>> transactionLog = new Dictionary<int, Dictionary<TransactionType, List<Transaction>>>();

        public void AddTransactionToLog(int fromAccNo, TransactionType transactionType, Transaction t)
        {
            if (!transactionLog.ContainsKey(fromAccNo))
            {
                transactionLog[fromAccNo] = new Dictionary<TransactionType, List<Transaction>>();
            }
            if (!transactionLog[fromAccNo].ContainsKey(transactionType))
            {
                transactionLog[fromAccNo][transactionType] = new List<Transaction>();
            }

            transactionLog[fromAccNo][transactionType].Add(t);
        }

        public List<Transaction> GetLogs(int accNo, TransactionType transactionType)
        {
            return transactionLog.ContainsKey(accNo) ? (transactionLog[accNo].ContainsKey(transactionType) ? transactionLog[accNo][transactionType] : null) : null;
        }

    }
}
