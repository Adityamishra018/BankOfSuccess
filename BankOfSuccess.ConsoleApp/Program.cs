using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankOfSuccess.Business;
using BankOfSuccess.Data.Entities;
using BankOfSuccess.Data;

namespace BankOfSuccess.ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            AccountForm form = new AccountForm();
            form.Render();
        }
    }

    public class AccountForm
    {
        IAccountManager accmgr = null;
        List<Account> accounts = new List<Account>();
        public AccountForm(IAccountManager accmgr)
        {
            this.accmgr = accmgr;
        }

        public AccountForm()
        {
            accmgr = AccountManager.GetInstance;
        }
        public void ShowAccounts(List<Account> accounts)
        {
            Console.WriteLine($"{"Sno",-5}{"Account No",-15}{"Type",-10}{"Name",-15}{"Balance",-10}{"Active",-10}\n");
            if(accounts == null)
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
            string name, gender, phoneno, company, regno, website;
            DateTime dob;
            int pin, from, to, mode;
            float amnt;

            accounts.Add(accmgr.OpenCurrentAccount("Abhishek", 1234, "Cognizant", "Cog.com", "123XDGS", 80000));
            accounts.Add(accmgr.OpenSavingsAccount("Aditya", 1234, "M", "72121221", new DateTime(1999, 10, 10), 1000));

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
                            accounts.Add(accmgr.OpenSavingsAccount(name, pin, gender, phoneno, dob));

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
                            accounts.Add(accmgr.OpenCurrentAccount(name, pin, company, website, regno));
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

                            if (accmgr.Deposit(accounts[from-1], amnt))
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

                            if (accmgr.Withdraw(accounts[from-1], amnt, pin))
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

                            if (accmgr.Transfer(accounts[from-1], accounts[to-1], amnt, pin, (TRANSFERMODE)mode))
                                Console.WriteLine($"Money Transfered Successfully using {(TRANSFERMODE)mode}");
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

                            if (accmgr.CloseAccount(accounts[from-1]))
                                Console.WriteLine("Account Closed");
                            else
                                Console.WriteLine("Something Went Wrong");

                            Console.Write("\nPress Enter to get to Main menu");
                            Console.ReadLine();
                            Console.Clear();
                            break;
                    }
                }
                catch (TransactionFailedException e)
                {
                    Console.WriteLine("EXCEPTION: " + e.Message);
                    Console.Write("\nPress Enter to get to Main menu");
                    Console.ReadLine();
                    Console.Clear();
                    ErrorLogger.WriteLog(e.Message);
                }
                catch (AccountException e)
                {
                    Console.WriteLine("EXCEPTION: " + e.Message);
                    Console.Write("\nPress Enter to get to Main menu");
                    Console.ReadLine();
                    Console.Clear();
                    ErrorLogger.WriteLog(e.Message);
                }
                catch(Exception ex)
                {
                    ErrorLogger.WriteLog(ex.Message);
                }

            }
        }

    }
}
