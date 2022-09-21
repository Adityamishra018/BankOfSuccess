using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankOfSuccessCS.Models;
using BankOfSuccessCS.Business.Core;
using System.Xml.Linq;
using BankOfSuccessCS.Business.Logging;
using System.Configuration;

namespace BankOfSuccessCS.UI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AccountForm form = new AccountForm();
            form.Render();
        }
    }

    public class AccountForm
    {
        IAccountManager accMgr;
        List<Account> accounts = new List<Account>();

        List<INotification> notifications = new List<INotification>();
        ILogManager logMgr;
        public AccountForm()
        {
            this.accMgr = AccountManagerFactory.GetAccountManager();
            this.logMgr = LogManagerFactory.GetLogManager();
        }
        public void ShowAccounts(List<Account> accounts)
        {
            Console.WriteLine($"{"Sno",-5}{"Account No",-15}{"Type",-10}{"Name",-15}{"Balance",-10}{"Active",-10}\n");
            if (accounts == null)
            {
                return;
            }
            int i = 1;
            foreach (var acc in accounts)
            {
                if (acc is SavingsAccount)
                {
                    Console.WriteLine($"{i++,-5}{acc.AccNo,-15}{"Savings",-10}{acc.Name,-15}{acc.Bal,-10}{acc.IsActive,-10}\n");
                }
                else
                    Console.WriteLine($"{i++,-5}{acc.AccNo,-15}{"Current",-10}{acc.Name,-15}{acc.Bal,-10}{acc.IsActive,-10}\n");
            }
        }

        public void Render()
        {
            string name, gender, phoneno, company, regno, website,mail;
            DateTime dob;
            int pin, from, to, mode;
            float amnt;

            accounts.Add(accMgr.OpenSavingsAccount("Aditya", 1234, "M", new DateTime(1998,10,10),"adi.adityamishra007@gmail.com", "+917389695954",50000));
            accounts.Add(accMgr.OpenCurrentAccount("Abhishek", 1234, "Cognizant", "Cog.Com", "10XSAS","abhisheksaurabh444@gmail.com", "+917389695954", 20000));

            while (true)
            {
                try
                {

                    Console.WriteLine("Bank Of Success Pvt Ltd");
                    Console.WriteLine("-----------------------------------");
                    Console.WriteLine("1. Add Savings Account");
                    Console.WriteLine("2. Add Current Account");
                    Console.WriteLine("3. View Accounts");
                    Console.WriteLine("4. Deposit Money");
                    Console.WriteLine("5. Withdraw Money");
                    Console.WriteLine("6. Transfer Money");
                    Console.WriteLine("7. Close Account");
                    Console.WriteLine("8. Subscribe To Notifications");


                    Console.Write("\nEnter choice: ");
                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            Console.Write("Enter Name: ");
                            name = Console.ReadLine();
                            Console.Write("Enter Gender: ");
                            gender = Console.ReadLine();
                            Console.Write("Enter DOB(DD-MM-YYYY): ");
                            dob = DateTime.Parse(Console.ReadLine());
                            Console.Write("Enter Phone No: ");
                            phoneno = Console.ReadLine();
                            Console.Write("Enter Preferred Pin: ");
                            pin = int.Parse(Console.ReadLine());
                            Console.Write("Enter Email: ");
                            mail = Console.ReadLine();
                            accounts.Add(accMgr.OpenSavingsAccount(name, pin, gender,dob,mail, phoneno));

                            Console.Write("\nPress Enter to get to Main menu");
                            Console.ReadLine();
                            Console.Clear();

                            break;

                        case "2":
                            Console.Write("Enter Name: ");
                            name = Console.ReadLine();
                            Console.Write("Enter Company: ");
                            company = Console.ReadLine();
                            Console.Write("Enter Registration No: ");
                            regno = Console.ReadLine();
                            Console.Write("Enter Website: ");
                            website = Console.ReadLine();
                            Console.Write("Enter Preferred Pin: ");
                            pin = int.Parse(Console.ReadLine());
                            Console.Write("Enter Email: ");
                            mail = Console.ReadLine();
                            Console.Write("Enter Phone No: ");
                            phoneno = Console.ReadLine();
                            accounts.Add(accMgr.OpenCurrentAccount(name, pin, company, website, regno,mail,phoneno));
                            Console.WriteLine("\nPress Enter to get to Main menu");
                            Console.ReadLine();
                            Console.Clear();
                            break;

                        case "3":
                            ShowAccounts(accounts);

                            Console.Write("\nPress Enter to get to Main menu");
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        case "4":
                            ShowAccounts(accounts);

                            Console.Write("Pick Account: ");
                            from = int.Parse(Console.ReadLine());

                            Console.Write("\nEnter Amount: ");
                            amnt = float.Parse(Console.ReadLine());

                            if (accMgr.Deposit(accounts[from - 1], amnt))
                                Console.WriteLine("Amount Deposited");

                            Console.Write("\nPress Enter to get to Main menu");
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        case "5":
                            ShowAccounts(accounts);

                            Console.Write("Pick Account: ");
                            from = int.Parse(Console.ReadLine());

                            Console.Write("\nEnter Amount: ");
                            amnt = float.Parse(Console.ReadLine());

                            Console.Write("\nEnter Pin: ");
                            pin = int.Parse(Console.ReadLine());

                            if (accMgr.Withdraw(accounts[from - 1], amnt, pin))
                                Console.WriteLine("Amount Withdrawn");
                            else
                                Console.WriteLine("Something Went Wrong!");

                            Console.Write("\nPress Enter to get to Main menu");
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        case "6":
                            ShowAccounts(accounts);

                            Console.Write("Pick From Account: ");
                            from = int.Parse(Console.ReadLine());

                            Console.Write("Pick To Account: ");
                            to = int.Parse(Console.ReadLine());

                            Console.Write("\nEnter Amount: ");
                            amnt = float.Parse(Console.ReadLine());

                            Console.WriteLine("\n1. IMPS ");
                            Console.WriteLine("2. NEFT ");
                            Console.WriteLine("3. RTGS ");
                            Console.Write("Pick Mode: ");
                            mode = int.Parse(Console.ReadLine());

                            Console.Write("\nEnter Pin: ");
                            pin = int.Parse(Console.ReadLine());

                            if (accMgr.Transfer(accounts[from - 1], accounts[to - 1], amnt, pin, (TransferMode)mode))
                                Console.WriteLine($"Money Transfered Successfully using {(TransferMode)mode}");
                            else
                                Console.WriteLine("Something Went Wrong!");

                            Console.Write("\nPress Enter to get to Main menu");
                            Console.ReadLine();
                            Console.Clear();
                            break;

                        case "7":
                            ShowAccounts(accounts);

                            Console.Write("Pick Account: ");
                            from = int.Parse(Console.ReadLine());

                            if (accMgr.CloseAccount(accounts[from - 1]))
                                Console.WriteLine("Account Closed");
                            else
                                Console.WriteLine("Something Went Wrong");

                            Console.Write("\nPress Enter to get to Main menu");
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        case "8":
                            ShowAccounts(accounts);

                            Console.Write("Pick Account: ");
                            from = int.Parse(Console.ReadLine());

                            Console.WriteLine("1. Email");
                            Console.WriteLine("2. SMS");
                            Console.Write("Pick Subscription Type: ");
                            mode = int.Parse(Console.ReadLine());

                            if (mode == 1)
                                accounts[from - 1].Subscribe(new EmailNotification());
                            else
                                accounts[from - 1].Subscribe(new SMSNotification());

                            Console.WriteLine("Subscription Added\n");
                            Console.Write("\nPress Enter to get to Main menu");
                            Console.ReadLine();
                            Console.Clear();
                            break;

                        default: break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception : " + ex.GetType() +  ex.Message);
                    logMgr.Log(ConfigurationManager.AppSettings["ErrorLogPath"], ex.GetType().Name);

                    Console.Write("\nPress Enter to get to Main menu");
                    Console.ReadLine();
                    Console.Clear();
                }
            }
        }
        private List<INotification> Subscribe()
        {
            List<INotification> list = new List<INotification>();
            Console.WriteLine("Subscribe Whatsapp? y/n?: ");
            char res = char.Parse(Console.ReadLine());
            if (res == 'y')
                list.Add(new WhatsappNotification());



            Console.WriteLine("Subscribe Email? y/n?");
            res = char.Parse(Console.ReadLine());
            if (res == 'y')
                list.Add(new EmailNotification());


            return list;
        }
    }
}
