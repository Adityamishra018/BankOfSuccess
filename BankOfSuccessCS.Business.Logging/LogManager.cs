using System.Configuration;
using System.IO;

namespace BankOfSuccessCS.Business.Logging
{
    public class LogManager : ILogManager
    {
        public void Log(string path,string message)
        {
            File.WriteAllText(path, message);
        }
    }
}
