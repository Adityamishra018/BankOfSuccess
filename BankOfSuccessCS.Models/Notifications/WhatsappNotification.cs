using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOfSuccessCS.Models
{
    public class WhatsappNotification : INotification
    {
        public void Update(string message)
        {
            throw new NotImplementedException();
        }

        public void Update(Transaction t)
        {
            throw new NotImplementedException();
        }
    }
}
