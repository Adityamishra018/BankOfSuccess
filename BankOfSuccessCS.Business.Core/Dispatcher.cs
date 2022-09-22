using BankOfSuccessCS.Models;

namespace BankOfSuccessCS.Business.Core
{
    public class Dispatcher
    {
        public void Dispatch(Card card)
        {
            card.Account.Notify("Your Card Is Dispatched," + card.Account.Email);
        }
    }
}
