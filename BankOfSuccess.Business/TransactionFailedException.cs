using System;
using System.Text;
using System.Threading.Tasks;

namespace BankOfSuccess.Business
{

    public class TransactionFailedException : ApplicationException
    {
        public TransactionFailedException(string message) : base(message)
        {
        }
    }

}
