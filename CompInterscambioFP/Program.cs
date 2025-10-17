using CompInterscambioFP.Managers;
using CompInterscambioFP.Models;
using CompInterscambioFP.Utilities;
using System;
using System.IO;

namespace CompInterscambioFP
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("hello world");
            //Console.Read();
            //IniParser fileIni = new IniParser(Path.Combine(Environment.CurrentDirectory, "") + @"\Settings.ini");
            //string ConnStr = fileIni.GetSetting("ROOT", "ConnStr");
            //string MECCANOPLASTICA_Percorso = fileIni.GetSetting("MECCANOPLASTICA", "Percorso");

            var manager = new SettingsManager(Path.Combine(Environment.CurrentDirectory, "") + @"\Settings.ini");
            Settings settings = manager.LoadSettings();

            Console.WriteLine(settings.ConnStr);
            Console.WriteLine(settings.MeccanoplasticaPercorso);
            Console.WriteLine(settings.MeccanoplasticaOld);

        }
    }
}
