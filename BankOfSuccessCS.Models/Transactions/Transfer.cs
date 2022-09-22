namespace BankOfSuccessCS.Models
{
    public class Transfer : Transaction
    {
        public Account ToAcc { get; set; }
        public TransferMode TransferMode { get; set; }

        public override string ToString()
        {
            return $"Transfer of {Amt} from Account No : {Acc.AccNo} to Account No {ToAcc.AccNo} on {Date}\n";
        }
    }
}
