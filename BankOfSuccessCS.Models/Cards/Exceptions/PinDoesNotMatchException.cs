using System;
using System.Runtime.Serialization;

namespace BankOfSuccessCS.Models
{
    [Serializable]
    internal class PinDoesNotMatchException : Exception
    {
        public PinDoesNotMatchException()
        {
        }

        public PinDoesNotMatchException(string message) : base(message)
        {
        }

        public PinDoesNotMatchException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PinDoesNotMatchException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

}
