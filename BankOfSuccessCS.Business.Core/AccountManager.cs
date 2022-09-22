using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
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
        ICardManager _cardManager;
        public AccountManager() 
        {
            _logger = LogManagerFactory.GetLogManager();
            _transactionManager = TransactionManagerFactory.GetTransactionManager();
            _cardManager = CardManagerFactory.GetCardManager();

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
                var card = (DebitCard)CardFactory.GetCard();
                card.Account = ac;
                _cardManager.AddCard(card);
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
                var card = (DebitCard)CardFactory.GetCard();
                card.Account = ac;
                _cardManager.AddCard(card);
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
                        _transactionManager.Add(acc.AccNo, TransactionType.WITHDRAWAL, withdrawal);
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
                _transactionManager.Add(acc.AccNo, TransactionType.DEPOSIT, deposit);

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
                        _transactionManager.Add(from.AccNo, TransactionType.TRANSFER, transfer);
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

        public bool GenerateStatement(Account acc)
        {
            List<Transaction> transactions = _transactionManager.Get(acc.AccNo,TransactionType.TRANSFER);
            transactions = transactions.Concat(_transactionManager.Get(acc.AccNo, TransactionType.WITHDRAWAL)).ToList();
            transactions = transactions.Concat(_transactionManager.Get(acc.AccNo, TransactionType.DEPOSIT)).ToList();

            string output="";
            foreach (var transc in transactions)
            {
                if(transc.GetType() == typeof(Transfer))
                {
                    var tr = transc as Transfer;
                    output += $"{tr.GetType().Name} : {tr.Acc.AccNo} to {tr.ToAcc} of Amount {tr.Amt} on {tr.Date}\n";
                }
                else
                    output += $"{transc.GetType().Name} : {transc.Acc.AccNo} of Amount {transc.Amt} on {transc.Date}\n";
            }

            Task.Run(() =>
            {
                StreamWriter writer = new StreamWriter(ConfigurationManager.AppSettings["StatementLogPath"]);
                writer.Write(output);
                writer.Close();
                acc.Notify($"{acc.Email},{acc.PhoneNo}");
            });

            return true;
        }

    }
}
