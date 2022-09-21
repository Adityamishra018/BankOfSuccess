using System.Configuration;
using System;

namespace BankOfSuccessCS.Business.Core
{
    public static class PrivilegeManagerFactory
    {
        public static IPrivilegeManager GetPrivilegeManager()
        {
            string type = ConfigurationManager.AppSettings["PrivilegeManager"];
            var t = Type.GetType($"{type}, BankOfSuccessCS.Business.Core");
            return (IPrivilegeManager)Activator.CreateInstance(t);
        }
    }

}
