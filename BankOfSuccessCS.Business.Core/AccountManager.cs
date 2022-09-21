using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankOfSuccessCS.Business.Logging;
using BankOfSuccessCS.Models;

namespace BankOfSuccessCS.Business.Core
{
    public class AccountManager : IAccountManager
    {
        ILogManager _logger;
        ITransactionManager _transactionManager;
        public AccountManager() 
        {
            _logger = LogManagerFactory.GetLogManager();
            _transactionManager = TransactionManagerFactory.GetTransactionManager();
        }
        public Account OpenSavingsAccount(string name, int pin, string gender, DateTime dob,string mail,string ph, int balance = 500)
        {
            SavingsAccount ac;
            if ((DateTime.Today - dob.Date).TotalDays >= 18 * 365)
            {
                ac = (SavingsAccount)AccountFactory.GetAccount(AccountType.SAVINGS,name, pin,mail,ph);
                ac.Gender = gender;
                ac.PhoneNo = ph;
                ac.DOB = dob;
                ac.Bal = balance;
                _transactionManager.Create(ac.AccNo);
                return ac;
            }
            else
                throw new AgeNotValidException();
        }

        public Account OpenCurrentAccount(string name, int pin, string company, string website, string regNo,string mail,string ph, int balance = 500)
        {
            CurrentAccount ac;
            if (regNo != null)
            {
                ac = (CurrentAccount)AccountFactory.GetAccount(AccountType.CURRENT, name, pin,mail,ph);
                ac.CompanyName = company;
                ac.Website = website;
                ac.RegistrationNo = regNo;
                ac.Bal = balance;
                _transactionManager.Create(ac.AccNo);
                return ac;
            }
            else
                throw new RegistrationIsNullExcepiton();
        }
        public bool CloseAccount(Account acc)
        {
            if (acc.IsActive == false)
                throw new AccountDoesNotExistException();
            if (acc.Bal > 0)
                throw new AccountCannotBeClosedException();

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
                        Withdrawal withdrawal = new Withdrawal { Acc = acc, Amt = amnt, Date = DateTime.Now };
                        acc.Notify(withdrawal);
                        _logger.Log(ConfigurationManager.AppSettings["TransactionLogPath"],withdrawal.ToString());
                        return true;
                    }
                    else
                        throw new InsufficientBalanceException();

                }
                else
                    throw new IncorrectPinException();
            }
            return false;
        }

        public bool Deposit(Account acc, float amnt)
        {
            if (acc.IsActive)
            {
                acc.Bal += amnt;
                Deposit deposit = new Deposit { Acc = acc, Amt = amnt, Date = DateTime.Now };
                acc.Notify(deposit);
                _logger.Log(ConfigurationManager.AppSettings["TransactionLogPath"],deposit.ToString());
                return true;
            }
            throw new AccountAlreadyClosedException();
        }

        public bool Transfer(Account from, Account to, float amnt, int pin, TransferMode mode)
        {
            if (from.IsActive & to.IsActive)
            {
                if (from.Bal < amnt)
                    throw new InsufficientBalanceException();

                if (from.Pin == pin)
                {
                    float total = 0;
                    var transactions = _transactionManager.Get(from.AccNo, TransactionType.TRANSFER);
                    total = transactions.Where(t => t.Date.Date == DateTime.Today).Sum(t => t.Amt);
                    if (total + amnt <= (float)from.Privilege)
                    {
                        from.Bal -= amnt;
                        to.Bal += amnt;
                        Transfer transfer = new Transfer { Acc = from, Amt = amnt, Date = DateTime.Now, ToAcc = to, TransferMode = mode };
                        from.Notify(transfer);
                        to.Notify(transfer);
                        _logger.Log(ConfigurationManager.AppSettings["TransactionLogPath"],transfer.ToString());
                        return true;
                    }
                    else
                        throw new LimitExceededException();

                }
                else
                    throw new IncorrectPinException();
            }
            else
                throw new AccountAlreadyClosedException();
        }
    }
}
