using BankOfSuccess.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOfSuccess.Data
{
    public class AccountFactory
    {
        private static int accCounter = 100;
        public readonly static AccountFactory GetInstance = new AccountFactory();
        private AccountFactory() { }
        public Account GetSavingsAccount(string name, int pin, string gender, string phno, DateTime dob, int balance = 500)
        {
            if ((DateTime.Now - dob).TotalDays > 6570)
            {
                return new SavingsAccount(accCounter++, name, pin, gender, phno, dob, balance);
            }
            else
                return null;
        }

        public Account GetCurrentAccount(string name, int pin, string company, string website, string regNo, int balance = 500)
        {
            if (regNo != null)
            {
                return new CurrentAccount(accCounter++, name, pin, company, website, regNo, balance);

            }
            else
                return null;
        }
    }
}
