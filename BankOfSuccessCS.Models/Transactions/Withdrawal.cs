namespace BankOfSuccessCS.Models
{
    public class Withdrawal : Transaction
    {
        public override string ToString()
        {
            return $"Withdrawal of {Amt} from Account No : {Acc.AccNo} on {Date}\n";
        }
    }
}
