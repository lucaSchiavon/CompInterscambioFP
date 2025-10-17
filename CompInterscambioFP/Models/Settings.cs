using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompInterscambioFP.Models
{
    public class Settings
    {
        // ROOT
        public string ConnStr { get; set; }

        // Sezione MECCANOPLASTICA
        public string MeccanoplasticaPercorso { get; set; }
        public string MeccanoplasticaOld { get; set; }
        public string MeccanoplasticaErrori { get; set; }

        // Sezione MECCANOPLASTICA4
        public string Meccanoplastica4Percorso { get; set; }
        public string Meccanoplastica4Old { get; set; }
        public string Meccanoplastica4Errori { get; set; }
    }
}
