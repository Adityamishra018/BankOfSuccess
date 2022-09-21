using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOfSuccess.Data
{
    public interface INotification
    {
        void Update(Transaction transaction);
    }
}
