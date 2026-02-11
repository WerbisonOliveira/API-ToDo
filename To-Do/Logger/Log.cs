using System;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace To_Do.Logger
{
    public class Log
    {
        public string Path { get; }

        public Log(string path)
        {
            Path = path;
        }

        public async Task Logger(Exception ex)
        {
            StringBuilder text = new StringBuilder();
            text.Append("Data: ");
            text.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            text.Append("\nMensagem: ");
            text.Append(ex.Message);
            text.Append("\nStackTrace: ");
            text.Append(ex.StackTrace);
            text.Append("\n---------------------------------------------------------------\n");

            using (StreamWriter log = new StreamWriter(Path, true))
            {
                await log.WriteAsync(text.ToString());
            }
        }
    }
}