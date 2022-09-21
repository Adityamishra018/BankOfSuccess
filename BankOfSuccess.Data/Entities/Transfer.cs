namespace BankOfSuccess.Data
{
    public enum TransferMode
    {
        IMPS, NEFT, RTGS
    }
    public class Transfer : Transaction
    {
        public int ToAccNo { get; set; }

        public TransferMode TransferMode { get; set; }
    }


}
