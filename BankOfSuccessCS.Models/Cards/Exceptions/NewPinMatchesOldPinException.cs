using System;
using System.Runtime.Serialization;

namespace BankOfSuccessCS.Models
{
    [Serializable]
    internal class NewPinMatchesOldPinException : Exception
    {
        public NewPinMatchesOldPinException()
        {
        }

        public NewPinMatchesOldPinException(string message) : base(message)
        {
        }

        public NewPinMatchesOldPinException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NewPinMatchesOldPinException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

}
