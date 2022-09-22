namespace BankOfSuccessCS.Models
{
    public class Deposit : Transaction
    {
        public override string ToString()
        {
            return $"Deposit of {Amt} to Account No : {Acc.AccNo} on {Date}\n";
        }
    }
}
