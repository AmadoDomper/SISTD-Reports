using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESql
{
    public class RepDesempIndividual
    {
        public int NumeroDeMeta { get; set; }
        public string Nombre { get; set; }
        public string ActiviOpe { get; set; }
        public string UniMed { get; set; }
        public int RespTareaId { get; set; }
        public string RespTarea { get; set; }
        public int nEsperado { get; set; }
        public int nLogrado { get; set; }
    }
}
