using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompInterscambioFP.Managers
{
    public class LogManager
    {
        private static readonly string LogDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log");

        public static void ScriviLog(string messaggio)
        {
            try
            {
                Directory.CreateDirectory(LogDir);
                string fileName = $"elaborazione_{DateTime.Now:dd_MM_yyyy__HH_mm_ss}.log";
                string filePath = Path.Combine(LogDir, fileName);
                File.AppendAllText(filePath, $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {messaggio}\n\n");
            }
            catch
            {
                // evitare crash su errori di scrittura log
            }
        }
    }
}
