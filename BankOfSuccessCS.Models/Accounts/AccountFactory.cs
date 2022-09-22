namespace BankOfSuccessCS.Models
{
    public static class AccountFactory
    {
        static int _counter = 1000; 
        public static Account GetAccount(AccountType type,string name,int pin,string mail,string ph)
        {
            if (type == AccountType.SAVINGS)
            {
                return new SavingsAccount(_counter++, name, pin,mail,ph);
            }
            else if(type == AccountType.CURRENT)
            {
                return new CurrentAccount(_counter++, name, pin,mail,ph);
            }
            return null;
        }
    }
}
