using BankOfSuccessCS.Models;

namespace BankOfSuccessCS.Business.Core
{
    public interface IPrivilegeManager
    {
        bool Change(Account a, Privilege p);
    }

}
