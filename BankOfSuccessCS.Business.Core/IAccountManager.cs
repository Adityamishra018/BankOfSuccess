using System;
using BankOfSuccessCS.Models;
namespace BankOfSuccessCS.Business.Core
{
    public interface IAccountManager
    {
        Account OpenSavingsAccount(string name, int pin, string gender, DateTime dob, string mail,string ph,int balance = 500);
        Account OpenCurrentAccount(string name, int pin, string company, string website, string regNo, string mail,string ph,int balance = 500);
        bool CloseAccount(Account acc);
        bool Withdraw(Account acc, float amnt, int pin);
        bool Deposit(Account acc, float amnt);
        bool Transfer(Account from, Account to, float amnt, int pin, TransferMode mode);
    }

}
