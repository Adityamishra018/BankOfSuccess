using System;
using System.Configuration;

namespace BankOfSuccessCS.Business.Core
{
    public static class CardManagerFactory
    {
        public static ICardManager GetCardManager()
        {
            string type = ConfigurationManager.AppSettings["CardManager"];
            var t = Type.GetType($"{type}, BankOfSuccessCS.Business.Core");
            return (ICardManager)Activator.CreateInstance(t);
        }
    }
}