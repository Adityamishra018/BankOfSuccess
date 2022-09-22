using System;

namespace BankOfSuccessCS.Models
{
    public abstract class Card
    {
        public long CardNo { get; set; }

        public int CVV { get; set; }

        public DateTime ExpiryDate { get; set; }

        public Status Status { get; set; }

        public int CardPin { get; set; }

        public Account Account { get; set; }

        public void SetCardPin(int pin)
        {
            CheckPinValid(pin);

            if (Status != Status.Dispatched)
            {
                throw new CardNotDispatchedException("Pin cannot be set as card is not dispatched.");
            }

            CardPin = pin;

            Status = Status.Active;

        }

        private void CheckPinValid(int pinNo)
        {
            if (pinNo.ToString().Length != 4 || pinNo < 0)
            {
                throw new InvalidPinException("This account could not be opened as the pin number is not valid.");
            }
        }

        public void ResetCardPin(int oldPin, int newPin)
        {
            CheckPinMatches(oldPin);

            CheckSamePin(oldPin, newPin);

            CheckPinValid(newPin);

            if (Status != Status.Dispatched)
            {
                throw new CardNotDispatchedException("Pin cannot be reset as card is not dispatched.");
            }


            CardPin = newPin;
        }

        private void CheckSamePin(int oldPin, int newPin)
        {
            if (oldPin == newPin)
            {
                throw new NewPinMatchesOldPinException("New pin cannot be same as old pin.");
            }
        }

        private void CheckPinMatches(int oldPin)
        {
            if (oldPin != CardPin)
            {
                throw new PinDoesNotMatchException("The pin entered does not match.");
            }
        }
    }

}
