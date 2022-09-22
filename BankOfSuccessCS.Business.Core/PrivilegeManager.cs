using BankOfSuccessCS.Models;

namespace BankOfSuccessCS.Business.Core
{
    public class PrivilegeManager : IPrivilegeManager
    {
        public bool Change(Account a, Privilege p)
        {
            if(a.Privilege != p)
            {
                a.Privilege = p;
                return true;
            }
            return false;
        }
    }

}
