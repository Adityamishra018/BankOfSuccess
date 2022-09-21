using System.Configuration;
using System;

namespace BankOfSuccessCS.Business.Core
{
    public static class TransactionManagerFactory
    {
        public static ITransactionManager GetTransactionManager()
        {
            string type = ConfigurationManager.AppSettings["TransactionManager"];
            var t = Type.GetType($"{type}, BankOfSuccessCS.Business.Core");
            return (ITransactionManager)Activator.CreateInstance(t);
        }
    }
}
