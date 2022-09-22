using System;
using System.Runtime.Serialization;

namespace BankOfSuccessCS.Models
{
    [Serializable]
    internal class CardNotDispatchedException : Exception
    {
        public CardNotDispatchedException()
        {
        }

        public CardNotDispatchedException(string message) : base(message)
        {
        }

        public CardNotDispatchedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CardNotDispatchedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

}
