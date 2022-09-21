using System;
using BankOfSuccess.Data;
using BankOfSuccess.Data.Entities;

namespace BankOfSuccess.Business
{
    public interface IAccountManager
    {
        void SetLogger(ILogManager logger);
        Account OpenSavingsAccount(string name, int pin, string gender, string phno, DateTime dob, int balance = 500);
        Account OpenCurrentAccount(string name, int pin, string company, string website, string regNo, int balance = 500);
        bool CloseAccount(Account acc);
        bool Withdraw(Account acc, float amnt, int pin);
        bool Deposit(Account acc, float amnt);
        bool Transfer(Account from, Account to, float amnt, int pin, TransferMode mode);
    }
}
