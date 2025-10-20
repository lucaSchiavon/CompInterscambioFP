using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompInterscambioFP.Models.CSV
{
    public class DatiMacchinaDto
    {
        public string DataOra { get; set; }
        public string Ricetta { get; set; }
        public float PezziBuoni { get; set; }
        public float PezziScarto { get; set; }

    }
}
