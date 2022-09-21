using System;

namespace BankOfSuccess.Data
{
    public class EmailNotification : INotification
    {
        public void Update(Transaction transaction)
        {
            Console.WriteLine("\nEmail Notification:\nTransaction type:" + transaction.GetType().Name + "\nAmount:" + transaction.Amt + "\nTimestamp:" + transaction.Date);
            if (transaction.GetType().Name == "Transfer")
            {
                Transfer transfer = transaction as Transfer;
                Console.WriteLine("From Account Number:" + transfer.AccNo + "\nTo Account Number:" + transfer.ToAccNo + "Transfer Mode:" + transfer.TransferMode);
            }
            else
            {
                Console.WriteLine("Account Number:" + transaction.AccNo);
            }
        }
    }
}
