using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;

namespace BankOfSuccess.Business
{
    public class LogManager : ILogTransactions
    {
        private Dictionary<int, Dictionary<TRANSACTIONTYPE, List<Transaction>>> loggers = new Dictionary<int, Dictionary<TRANSACTIONTYPE, List<Transaction>>>();

        Type GetClassType(TRANSACTIONTYPE t)
        {
            if (t == TRANSACTIONTYPE.DEPOSIT)
                return typeof(Deposit);
            else if (t == TRANSACTIONTYPE.WITHDRAW)
                return typeof(WithDraw);
            else
                return typeof(Transfer);
        }

        public void CreateLog(int accNo)
        {
            if (loggers.ContainsKey(accNo) == false)
            {
                var dict = new Dictionary<TRANSACTIONTYPE, List<Transaction>>();
                dict.Add(TRANSACTIONTYPE.DEPOSIT, new List<Transaction>());
                dict.Add(TRANSACTIONTYPE.WITHDRAW, new List<Transaction>());
                dict.Add(TRANSACTIONTYPE.TRANSFER, new List<Transaction>());
                loggers.Add(accNo, dict);
            }
        }

        public void UpdateLog(int from, float amt,TRANSACTIONTYPE type,int to=0)
        {
            var t = (Transaction)Activator.CreateInstance(GetClassType(type));
            t.Acc = from;
            t.Amt = amt;
            t.Date = DateTime.Now;
            if (GetClassType(type) == typeof(Transfer))
            {
                var tr = t as Transfer;
                tr.To = to;
                loggers[from][type].Add(tr as Transaction);
                return;
            }

            loggers[from][type].Add(t as Transaction);
            return;
        }

        public List<Transaction> GetLogs(int accNo,TRANSACTIONTYPE t) 
        {
            return loggers[accNo][t];
        }

    }
}
