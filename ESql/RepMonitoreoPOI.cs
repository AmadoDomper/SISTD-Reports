using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESql
{
    public class RepMonitoreoPOI
    {
        public int InstanciaId { get; set; }
        public int PlanOperativoId { get; set; }
        public int? nAvance1 { get; set; }
        public int? nMotivoRestraso1 { get; set; }
        public string cLogro1 { get; set; }
        public int? nAvance2 { get; set; }
        public int? nMotivoRestraso2 { get; set; }
        public string cLogro2 { get; set; }
        public int? nAvance3 { get; set; }
        public int? nMotivoRestraso3 { get; set; }
        public string cLogro3 { get; set; }
    }
}
