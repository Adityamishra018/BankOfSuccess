using BankOfSuccessCS.Models;
using System.Collections.Generic;

namespace BankOfSuccessCS.Business.Core
{
    public interface ITransactionManager
    {
        void Add(int fromAccNo, TransactionType transactionType, Transaction t);
        void Create(int fromAccNo);
        List<Transaction> Get(int accNo, TransactionType t);
    }

}
