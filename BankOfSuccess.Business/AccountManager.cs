using System;
using System.Linq;
using BankOfSuccess.Data.Entities;
using BankOfSuccess.Data;

namespace BankOfSuccess.Business
{
    public enum TRANSFERMODE
    {
        IMPS, NEFT, RTGS
    }
    public sealed class AccountManager : IAccountManager
    {
        private ILogTransactions _logger = new LogManager();
        public static readonly AccountManager GetInstance = new AccountManager();
        AccountFactory _factory = AccountFactory.GetInstance;
        private AccountManager() { }
        public void SetLogger(ILogTransactions logger)
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
                return true;
            }
            throw new AccountException("Account Closed Cannot Deposit");
        }

        public bool Transfer(Account from, Account to, float amnt, int pin, TRANSFERMODE mode)
        {
            if (from.IsActive & to.IsActive)
            {
                if (from.Bal < amnt)
                    throw new AccountException("Payee doesn't have enough balance");

                if (from.Pin == pin)
                {
                    var transactions = _logger.GetLogs(from.AccNo,TRANSACTIONTYPE.TRANSFER);
                    float total = transactions.Where(t => t.Date.Date == DateTime.Today).Sum(t => t.Amt);

                    if (total + amnt <= (float)from.Privelege)
                    {
                        from.Bal -= amnt;
                        to.Bal += amnt;
                        _logger.UpdateLog(from.AccNo, amnt,TRANSACTIONTYPE.TRANSFER,to.AccNo);
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
