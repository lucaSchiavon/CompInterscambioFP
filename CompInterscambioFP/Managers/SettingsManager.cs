using CompInterscambioFP.Models;
using CompInterscambioFP.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompInterscambioFP.Managers
{
    public class SettingsManager
    {
        private IniParser parser;

        public SettingsManager(string iniFilePath)
        {
            parser = new IniParser(iniFilePath);
        }

        public Settings LoadSettings()
        {
            var settings = new Settings();

            // ROOT
            settings.ConnStr = parser.GetSetting("ROOT", "ConnStr");

            // MECCANOPLASTICA
            settings.MeccanoplasticaPercorso = parser.GetSetting("MECCANOPLASTICA", "Percorso");
            settings.MeccanoplasticaOld = parser.GetSetting("MECCANOPLASTICA", "Old");
            settings.MeccanoplasticaErrori = parser.GetSetting("MECCANOPLASTICA", "Errori");

            // MECCANOPLASTICA4
            settings.Meccanoplastica4Percorso = parser.GetSetting("MECCANOPLASTICA4", "Percorso");
            settings.Meccanoplastica4Old = parser.GetSetting("MECCANOPLASTICA4", "Old");
            settings.Meccanoplastica4Errori = parser.GetSetting("MECCANOPLASTICA4", "Errori");

            return settings;
        }
    }
}
