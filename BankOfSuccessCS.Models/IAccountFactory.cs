using System;

namespace BankOfSuccessCS.Models
{
    public interface IAccountFactory
    {
        Account GetSavingsAccount(string name, int pin, string gender, DateTime dob, string phno, int balance = 500);
        Account GetCurrentAccount(string name, int pin, string company, string website, string regNo, string phno,int balance = 500);
    }
}
