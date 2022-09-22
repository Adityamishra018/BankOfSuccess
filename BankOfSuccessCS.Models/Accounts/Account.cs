using System;
using System.Collections.Generic;

namespace BankOfSuccessCS.Models
{
    public abstract class Account : INotify
    {
        public int AccNo { get; private set; }
        public string Name { get; set; }
        public int Pin { get; private set; }
        public float Bal { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }

        public DateTime OpeningDate { get; set; }
        public DateTime ClosingDate { get; set; }
        public Privilege Privilege { get; set; } = Privilege.SILVER;
        public bool IsActive { get; set; } = true;

        private List<INotification> observers = new List<INotification>();

        public void Subscribe(INotification notification)
        {
            observers.Add(notification);
        }
        public void UnSubscribe(INotification notification)
        {
            observers.Remove(notification);
        }

        public void Notify(string msg)
        {
            foreach (var obs in observers)
            {
                obs.Update(msg);
            }
        }

        public void Notify(Transaction t)
        {
            foreach (var obs in observers)
            {
                obs.Update(t);
            }
        }

        public Account(int accNo, string name, int pin, string mail, string phoneNo)
        {
            AccNo = accNo;
            Name = name;
            Pin = pin;
            Bal = 500;
            OpeningDate = DateTime.Now;
            Email = mail;
            PhoneNo = phoneNo;
        }
    }
}
