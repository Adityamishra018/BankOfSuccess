using BankOfSuccessCS.Models;

namespace BankOfSuccessCS.Business.Core
{
    public interface ICardManager
    {
        void AddCard(Card card);
        void DispatchCards();
    }
}
