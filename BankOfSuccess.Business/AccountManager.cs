using System;
using System.Linq;
using BankOfSuccess.Data.Entities;
using BankOfSuccess.Data;
using System.Security.Principal;

namespace BankOfSuccess.Business
{

    public sealed class AccountManager : IAccountManager
    {
        private ILogManager _logger = new LogManager();
        public static readonly AccountManager GetInstance = new AccountManager();
        AccountFactory _factory = AccountFactory.GetInstance;
        private AccountManager() { }
        public void SetLogger(ILogManager logger)
        {
            _logger = logger;
        }
        public Account OpenSavingsAccount(string name, int pin, string gender, string phno, DateTime dob, int balance = 500)
        {
            Account ac;
            if ((DateTime.Today - dob.Date).TotalDays >= 18 * 365)
            {
                ac = _factory.GetSavingsAccount(name, pin, gender, phno, dob, balance);
                return ac;

            }
            else
                throw new AccountException("Age Should be greater than 18");
        }

        public Account OpenCurrentAccount(string name, int pin, string company, string website, string regNo, int balance = 500)
        {
            Account ac;
            if (regNo != null)
            {
                ac = _factory.GetCurrentAccount(name, pin, company, website, regNo, balance = 500);
                return ac;
            }
            else
                throw new AccountException("RegistraionNo cannot be null");
        }
        public bool CloseAccount(Account acc)
        {
            if (acc.IsActive == false)
                throw new AccountException("Account Doesn't Exist or is already closed");
            if (acc.Bal > 0)
                throw new AccountException("Cannot close account balance is not zero");

            acc.IsActive = false;
            acc.ClosingDate = DateTime.Now;
            return true;
        }

        public bool Withdraw(Account acc, float amnt, int pin)
        {
            if (acc.IsActive)
            {
                if (acc.Pin == pin)
                {
                    if (acc.Bal >= amnt)
                    {
                        acc.Bal -= amnt;
                        Withdrawal withdrawal = new Withdrawal { AccNo = acc.AccNo, Amt = amnt, Date = DateTime.Now };
                        _logger.AddTransactionToLog(acc.AccNo, TransactionType.Withdrawal, withdrawal);
                        acc.Notify(withdrawal);
                        return true;
                    }
                    else
                        throw new TransactionFailedException("Insufficient Balance");

                }
                else
                    throw new TransactionFailedException("Invalid Pin");
            }
            return false;
        }

        public bool Deposit(Account acc, float amnt)
        {
            if (acc.IsActive)
            {
                acc.Bal += amnt;
                Deposit deposit = new Deposit { AccNo = acc.AccNo, Amt = amnt, Date = DateTime.Now };
                _logger.AddTransactionToLog(acc.AccNo, TransactionType.Deposit, deposit);
                acc.Notify(deposit);
                return true;
            }
            throw new AccountException("Account Closed Cannot Deposit");
        }

        public bool Transfer(Account from, Account to, float amnt, int pin, TransferMode mode)
        {
            if (from.IsActive & to.IsActive)
            {
                if (from.Bal < amnt)
                    throw new AccountException("Payee doesn't have enough balance");

                if (from.Pin == pin)
                {
                    var transactions = _logger.GetLogs(from.AccNo, TransactionType.Transfer);
                    float total = 0;
                    if (transactions != null)
                    {
                        total = transactions.Where(t => t.Date.Date == DateTime.Today).Sum(t => t.Amt);
                    }
                    if (total + amnt <= (float)from.Privilege)
                    {
                        from.Bal -= amnt;
                        to.Bal += amnt;
                        Transfer transfer = new Transfer { AccNo = from.AccNo, Amt = amnt, Date = DateTime.Now, ToAccNo = to.AccNo, TransferMode = mode };
                        _logger.AddTransactionToLog(from.AccNo, TransactionType.Transfer, transfer);
                        to.Notify(transfer);
                        from.Notify(transfer);
                        return true;
                    }
                    else
                        throw new TransactionFailedException("Cannot Exceed Daily Limit");

                }
                else
                    throw new TransactionFailedException("Invalid Pin");
            }
            else
                throw new AccountException("Account is not Active");
        }
    }
}
