using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOfSuccess.Data.Entities
{
    public enum PRIVILEGE
    {
        SILVER = 25000, GOLD = 50000, PREMIUM = 100000
    }
    public abstract class Account
    {

        public int AccNo { get; private set; }
        public string Name { get; set; }
        public int Pin { get; private set; }
        public float Bal { get; set; }
        public DateTime OpeningDate { get; set; }
        public DateTime ClosingDate { get; set; }
        public PRIVILEGE Privelege { get; set; } = PRIVILEGE.SILVER;
        public bool IsActive { get; set; } = true;

        public Account(int accNo, string name, int pin, float bal, DateTime op)
        {
            AccNo = accNo;
            Name = name;
            Pin = pin;
            Bal = bal;
            OpeningDate = op;
        }
    }
}
