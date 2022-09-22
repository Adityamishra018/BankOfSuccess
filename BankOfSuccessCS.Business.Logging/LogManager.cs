using System.Configuration;
using System.IO;

namespace BankOfSuccessCS.Business.Logging
{
    public class LogManager : ILogManager
    {
        public void Log(string path,string message)
        {
            StreamWriter writer = new StreamWriter(path,true);
            writer.Write(message);
            writer.Close();
        }
    }
}
