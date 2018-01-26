using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using ADSql.Helper;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ESql;

namespace ADSql
{
    public class AvanceInformeAD
    {
        private Database oDatabase = DatabaseFactory.CreateDatabase(Conexion.nombre);
        public List<Combo> ListaPOIVigentesCombo()
        {
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.uspPlanOperativoVigentesSELListaParaCombo);
            List<Combo> ls=new List<Combo>();
            using (IDataReader datos = oDatabase.ExecuteReader(oDbCommand))
            {
                while (datos.Read())
                {
                    Combo item = new Combo();
                    item.CValue =  DataUtil.DbValueToDefault<Int32>(datos[datos.GetOrdinal("ComboValue")]).ToString();
                    item.CText = DataUtil.DbValueToDefault<String>(datos[datos.GetOrdinal("ComboText")]);
                    ls.Add(item);
                }
            }
            return ls;
        }
        public List<Combo> ListarMetasDeResponsableMeta(Int32 PlanOperativoId, Int32 ResponsableMeta)
        {
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.uspMetasPlanOperativoxUsuarioResponsableMetaSELParaCombo);
            oDatabase.AddInParameter(oDbCommand, "@PlanOperativoId", DbType.String, PlanOperativoId);
            oDatabase.AddInParameter(oDbCommand, "@ResponsableMeta", DbType.String, ResponsableMeta);
            List<Combo> ls=new List<Combo>();
            using (IDataReader datos = oDatabase.ExecuteReader(oDbCommand))
            {
                while (datos.Read())
                {
                    Combo item = new Combo();
                    item.CValue = DataUtil.DbValueToDefault<Int32>(datos[datos.GetOrdinal("InstanciaDeMetaId")]).ToString();
                    item.CText = DataUtil.DbValueToDefault<String>(datos[datos.GetOrdinal("Meta")]);
                    ls.Add(item);
                }
            }
            return ls;
        }
        public string registrarAvance(RepMonitoreoPOI item, int nPeriodo)
        {
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.uspReporteMonitoreoPoiIns);
            oDatabase.AddInParameter(oDbCommand, "@InstanciaId", DbType .Int32, item.InstanciaId);
            oDatabase.AddInParameter(oDbCommand, "@PlanOperativoId", DbType.Int32, item.PlanOperativoId);
            oDatabase.AddInParameter(oDbCommand, "@nAvance1", DbType.Int32, item.nAvance1);
            oDatabase.AddInParameter(oDbCommand, "@nMotivoRestraso1", DbType.Int32,item.nMotivoRestraso1);
            oDatabase.AddInParameter(oDbCommand, "@cLogro1", DbType.String, item.cLogro1);
            oDatabase.AddInParameter(oDbCommand, "@nAvance2", DbType.Int32, item.nAvance2);
            oDatabase.AddInParameter(oDbCommand, "@nMotivoRestraso2", DbType.Int32, item.nMotivoRestraso2);
            oDatabase.AddInParameter(oDbCommand, "@cLogro2", DbType.String, item.cLogro2);
            oDatabase.AddInParameter(oDbCommand, "@nAvance3", DbType.Int32, item.nAvance3);
            oDatabase.AddInParameter(oDbCommand, "@nMotivoRestraso3", DbType.Int32, item.nMotivoRestraso3);
            oDatabase.AddInParameter(oDbCommand, "@cLogro3", DbType.String, item.cLogro3);
            oDatabase.AddInParameter(oDbCommand, "@nPeriodo", DbType.Int32, nPeriodo);
            using (IDataReader datos = oDatabase.ExecuteReader(oDbCommand))
            {
                if (datos.Read())
                {
                    return DataUtil.DbValueToDefault<string>(datos[datos.GetOrdinal("res")]);
                }
            }
            return "";
        }
        public List<MatrizProgramacionFisica> ListaMatrizDeProgramacionFisica(int InstanciaId, int nResponsableId )
        {
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.uspMatrizProgramacionFisica);
            oDatabase.AddInParameter(oDbCommand, "@InstanciaId", DbType.String, InstanciaId);
            oDatabase.AddInParameter(oDbCommand, "@ResponsableId", DbType.String, nResponsableId);
            List<MatrizProgramacionFisica> ls = new List<MatrizProgramacionFisica>();
            using (IDataReader datos = oDatabase.ExecuteReader(oDbCommand))
            {
                while (datos.Read())
                {
                    MatrizProgramacionFisica item = new MatrizProgramacionFisica();
                    item.Nivel = DataUtil.DbValueToDefault<Int32>(datos[datos.GetOrdinal("Nivel")]);
                    item.ID = DataUtil.DbValueToDefault<Int32>(datos[datos.GetOrdinal("ID")]);
                    item.ID_PADRE = DataUtil.DbValueToDefault<Int32>(datos[datos.GetOrdinal("ID_PADRE")]);
                    item.EstructuraDeMetaNivel1Id = DataUtil.DbValueToDefault<Int32>(datos[datos.GetOrdinal("EstructuraDeMetaNivel1Id")]);
                    item.Nombre = DataUtil.DbValueToDefault<String>(datos[datos.GetOrdinal("Nombre")]);
                    item.UnidadDeMedidaId = DataUtil.DbValueToDefault<Int32>(datos[datos.GetOrdinal("UnidadDeMedidaId")]);
                    item.UnidadDeMedida = DataUtil.DbValueToDefault<String>(datos[datos.GetOrdinal("UnidadDeMedida")]);
                    item.Ene = DataUtil.DbValueToDefault<Decimal>(datos[datos.GetOrdinal("Ene")]);
                    item.Feb = DataUtil.DbValueToDefault<Decimal>(datos[datos.GetOrdinal("Feb")]);
                    item.Mar = DataUtil.DbValueToDefault<Decimal>(datos[datos.GetOrdinal("Mar")]);
                    item.Abr = DataUtil.DbValueToDefault<Decimal>(datos[datos.GetOrdinal("Abr")]);
                    item.May = DataUtil.DbValueToDefault<Decimal>(datos[datos.GetOrdinal("May")]);
                    item.Jun = DataUtil.DbValueToDefault<Decimal>(datos[datos.GetOrdinal("Jun")]);
                    item.nTotal_I_S = DataUtil.DbValueToDefault<Decimal>(datos[datos.GetOrdinal("Total_I_S")]);
                    item.Jul = DataUtil.DbValueToDefault<Decimal>(datos[datos.GetOrdinal("Jul")]);
                    item.Ago = DataUtil.DbValueToDefault<Decimal>(datos[datos.GetOrdinal("Ago")]);
                    item.Sep = DataUtil.DbValueToDefault<Decimal>(datos[datos.GetOrdinal("Sep")]);
                    item.nTotal_III_T = DataUtil.DbValueToDefault<Decimal>(datos[datos.GetOrdinal("Total_III_T")]);
                    item.Oct = DataUtil.DbValueToDefault<Decimal>(datos[datos.GetOrdinal("Oct")]);
                    item.Nov = DataUtil.DbValueToDefault<Decimal>(datos[datos.GetOrdinal("Nov")]);
                    item.Dic = DataUtil.DbValueToDefault<Decimal>(datos[datos.GetOrdinal("Dic")]);
                    item.nTotal_IV_T = DataUtil.DbValueToDefault<Decimal>(datos[datos.GetOrdinal("Total_IV_T")]);
                    item.TotalMeta = DataUtil.DbValueToDefault<Decimal>(datos[datos.GetOrdinal("TotalMeta")]);

                    item.nAvance1 = DataUtil.toIntDefault(datos[datos.GetOrdinal("nAvance1")]);
                    item.nMotivoRestraso1 = DataUtil.toIntDefault(datos[datos.GetOrdinal("nMotivoRestraso1")]);
                    item.cMotivoRestraso1 = DataUtil.DbValueToDefault<String>(datos[datos.GetOrdinal("cMotivoRestraso1")]);
                    item.cLogro1 = DataUtil.DbValueToDefault<String>(datos[datos.GetOrdinal("cLogro1")]);

                    item.nAvance2 = DataUtil.toIntDefault(datos[datos.GetOrdinal("nAvance2")]);
                    item.nMotivoRestraso2 = DataUtil.toIntDefault(datos[datos.GetOrdinal("nMotivoRestraso2")]);
                    item.cMotivoRestraso2 = DataUtil.DbValueToDefault<String>(datos[datos.GetOrdinal("cMotivoRestraso2")]);
                    item.cLogro2 = DataUtil.DbValueToDefault<String>(datos[datos.GetOrdinal("cLogro2")]);

                    item.nAvance3 = DataUtil.toIntDefault(datos[datos.GetOrdinal("nAvance3")]);
                    item.nMotivoRestraso3 = DataUtil.toIntDefault(datos[datos.GetOrdinal("nMotivoRestraso3")]);
                    item.cMotivoRestraso3 = DataUtil.DbValueToDefault<String>(datos[datos.GetOrdinal("cMotivoRestraso3")]);
                    item.cLogro3 = DataUtil.DbValueToDefault<String>(datos[datos.GetOrdinal("cLogro3")]);

                    ls.Add(item);
                }
            }
            return ls;
        }

        public List<MatrizProgramacionFisica> ordenarMatrizPorPadre(List<MatrizProgramacionFisica> lista)
        {
            List<MatrizProgramacionFisica> Ordenada = new List<MatrizProgramacionFisica>();
            List<MatrizProgramacionFisica> padres = lista.Where(x => x.ID_PADRE==0).ToList();
            List<MatrizProgramacionFisica> hijos = lista.Where(x => x.ID_PADRE != 0).ToList();
            foreach(MatrizProgramacionFisica itemPadre in padres)
            {
                Ordenada.Add(itemPadre);
                foreach (MatrizProgramacionFisica itemHijo in hijos)
                {
                    if (itemHijo.ID_PADRE == itemPadre.EstructuraDeMetaNivel1Id)
                    {
                        Ordenada.Add(itemHijo);
                    }
                }
            }

            //Ordenada.OrderBy(x => x.Nombre).ToList<MatrizProgramacionFisica>();

            return Ordenada.OrderBy(x => x.Nombre).ToList<MatrizProgramacionFisica>();
        }

        public List<Combo> ListaMotivosDeRestraso()
        {
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.uspMotivoRestrasoSel);
            List<Combo> ls = new List<Combo>();
            using (IDataReader datos = oDatabase.ExecuteReader(oDbCommand))
            {
                while (datos.Read())
                {
                    Combo item = new Combo();
                    item.CValue = DataUtil.toIntDefault(datos[datos.GetOrdinal("Valor")]).ToString();
                    item.CText = DataUtil.DbValueToDefault<String>(datos[datos.GetOrdinal("Texto")]);
                    ls.Add(item);
                }
            }
            return ls;
        }

        public Indicadores OptenerIndicadores(int nInstancia,int nUserId, int nPeriodo)
        {
            Indicadores oIndicadores = new Indicadores();
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.uspOptenerIndicadores);
            oDatabase.AddInParameter(oDbCommand, "@InstanciaId", DbType.Int32, nInstancia);
            oDatabase.AddInParameter(oDbCommand, "@ResponsableId", DbType.Int32, nUserId);
            oDatabase.AddInParameter(oDbCommand, "@nPeriodo", DbType.Int32, nPeriodo);

            using (IDataReader datos = oDatabase.ExecuteReader(oDbCommand))
            {
                while (datos.Read())
                {

                    oIndicadores.Eficacia = DataUtil.toIntDefault(datos[datos.GetOrdinal("Eficacia")]);
                    oIndicadores.Avance = DataUtil.toIntDefault(datos[datos.GetOrdinal("Avance")]);
                }
            }

            return oIndicadores;
        }

        //public Indicadores OptenerEficaciaFisica(int nInstancia, int nUserId)
        //{
        //    Indicadores oEficaFi = new Indicadores();
        //    DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.uspOptenerEficaciaFisica);
        //    oDatabase.AddInParameter(oDbCommand, "@InstanciaId", DbType.Int32, nInstancia);
        //    oDatabase.AddInParameter(oDbCommand, "@ResponsableId", DbType.Int32, nUserId);

        //    using (IDataReader datos = oDatabase.ExecuteReader(oDbCommand))
        //    {
        //        while (datos.Read())
        //        {

        //            oEficaFi.AvaFisPer1 = DataUtil.toIntDefault(datos[datos.GetOrdinal("AvaFisPer1")]);
        //            oEficaFi.AvaFisPer2 = DataUtil.toIntDefault(datos[datos.GetOrdinal("AvaFisPer2")]);
        //            oEficaFi.AvaFisPer3 = DataUtil.toIntDefault(datos[datos.GetOrdinal("AvaFisPer3")]);
        //        }
        //    }
        //    return oEficaFi;
        //}

        //public List<ReporteEfiCatePre> ListaReporteEfiCatePre_I_S()
        //{
        //    DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.uspRepEficaxCatPresu_I_Semestre);
        //    List<ReporteEfiCatePre> ls = new List<ReporteEfiCatePre>();
        //    using (IDataReader datos = oDatabase.ExecuteReader(oDbCommand))
        //    {
        //        while (datos.Read())
        //        {
        //            ReporteEfiCatePre item = new ReporteEfiCatePre();
        //            item.CodigoAgrupacionProgramatica = DataUtil.DbValueToDefault<String>(datos[datos.GetOrdinal("CodigoAgrupacionProgramatica")]);
        //            item.CatePresu = DataUtil.DbValueToDefault<String>(datos[datos.GetOrdinal("CatePresu")]);
        //            item.AgrupacionProgramaticaId = DataUtil.DbValueToDefault<Int32>(datos[datos.GetOrdinal("AgrupacionProgramaticaId")]);
        //            item.ActProgra = DataUtil.DbValueToDefault<String>(datos[datos.GetOrdinal("ActProgra")]);
        //            item.APEficacia = DataUtil.DbValueToDefault<Int32>(datos[datos.GetOrdinal("APEficacia")]);
        //            item.APCateEficacia = DataUtil.DbValueToDefault<Int32>(datos[datos.GetOrdinal("APCateEficacia")]);
        //            ls.Add(item);
        //        }
        //    }
        //    return ls;
        //}


    }
}
