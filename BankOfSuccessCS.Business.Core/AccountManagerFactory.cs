using BankOfSuccessCS.Models;
using System;
using System.Configuration;

namespace BankOfSuccessCS.Business.Core
{
    public static class AccountManagerFactory
    {
        public static IAccountManager GetAccountManager()
        {
            string type = ConfigurationManager.AppSettings["AccountManager"];
            var t = Type.GetType($"{type}, BankOfSuccessCS.Business.Core");
            return (IAccountManager)Activator.CreateInstance(t);
        }
    }
}
