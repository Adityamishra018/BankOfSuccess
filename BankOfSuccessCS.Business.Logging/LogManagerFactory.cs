using System;
using System.Configuration;

namespace BankOfSuccessCS.Business.Logging
{
    public static class LogManagerFactory
    {
      
        public static ILogManager GetLogManager()
        {
            string type = ConfigurationManager.AppSettings["LogManager"];
            var t = Type.GetType($"{type}, BankOfSuccessCS.Business.Logging");
            return (ILogManager)Activator.CreateInstance(t);
        }
    }
}
