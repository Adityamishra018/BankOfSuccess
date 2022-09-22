using System;

namespace BankOfSuccessCS.Models
{
    public static class CardFactory
    {
        static long counter = 4212457612345438L;
        static int cvv = 588;
        public static Card GetCard()
        {
            return new DebitCard { CardNo = counter++, CVV = cvv++, ExpiryDate = (DateTime.Now + new TimeSpan(1460, 12, 22, 15)) };
        }
    }

}
