using System.Collections.Generic;
using BankOfSuccess.Data;

namespace BankOfSuccess.Business
{
    public interface ILogManager
    {
        void AddTransactionToLog(int fromAccNo, TransactionType transactionType, Transaction t);
        List<Transaction> GetLogs(int accNo, TransactionType t);
    }


}
