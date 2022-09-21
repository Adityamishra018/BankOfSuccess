using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOfSuccessCS.Business.Logging
{
    public interface ILogManager
    {
        void Log(string path, string message);
    }

}
