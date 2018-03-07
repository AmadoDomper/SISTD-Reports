using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESql
{
    public class MatrizProgramacionFisica
    {
        public int Nivel { get; set; }
        public int ID { get; set; }
        public int? ID_PADRE { get; set; }
        public int EstructuraDeMetaNivel1Id { get; set; }
        public string Nombre { get; set; }
        public string UnidadDeMedida { get; set; }
        public int UnidadDeMedidaId { get; set; }
        public bool? Eliminado { get; set; }
        public bool? Existe { get; set; }
        public decimal Ene { get; set; }
        public decimal Feb { get; set; }
        public decimal Mar { get; set; }
        public decimal Abr { get; set; }
        public decimal May { get; set; }
        public decimal Jun { get; set; }
        public decimal Jul { get; set; }
        public decimal Ago { get; set; }
        public decimal Sep { get; set; }
        public decimal Oct { get; set; }
        public decimal Nov { get; set; }
        public decimal Dic { get; set; }
        public decimal TotalMeta { get; set; }
        public int? nAvance1 { get; set; }
        public int? nAvance2 { get; set; }
        public int? nAvance3 { get; set; }
        public int? nAvance4 { get; set; }
        public int? nMotivoRestraso1 { get; set; }
        public int? nMotivoRestraso2 { get; set; }
        public int? nMotivoRestraso3 { get; set; }
        public int? nMotivoRestraso4 { get; set; }

        public string cMotivoRestraso1 { get; set; }
        public string cMotivoRestraso2 { get; set; }
        public string cMotivoRestraso3 { get; set; }
        public string cMotivoRestraso4 { get; set; }

        public string cLogro1 { get; set; }
        public string cLogro2 { get; set; }
        public string cLogro3 { get; set; }
        public string cLogro4 { get; set; }
        public decimal nTotal_I_S { get; set; }
        public decimal nTotal_I_T { get; set; }
        public decimal nTotal_II_T { get; set; }
        public decimal nTotal_III_T { get; set; }
        public decimal nTotal_IV_T { get; set; }
    }
}
