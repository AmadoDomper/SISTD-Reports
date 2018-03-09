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
    public class ReportesAD
    {
        private Database oDatabase = DatabaseFactory.CreateDatabase(Conexion.nombre);

        public List<ReporteEfiCatePre> ListaReporteEfiCatePre(int nPeriodo, int nPlanOpeId)
        {
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.uspRepEficaxCatPresu);
            oDatabase.AddInParameter(oDbCommand, "@nPeriodo", DbType.Int32, nPeriodo);
            oDatabase.AddInParameter(oDbCommand, "@nPlanOpeId", DbType.Int32, nPlanOpeId);

            List<ReporteEfiCatePre> ls = new List<ReporteEfiCatePre>();
            using (IDataReader datos = oDatabase.ExecuteReader(oDbCommand))
            {
                while (datos.Read())
                {
                    ReporteEfiCatePre item = new ReporteEfiCatePre();
                    item.CodigoAgrupacionProgramatica = DataUtil.DbValueToDefault<String>(datos[datos.GetOrdinal("CodigoAgrupacionProgramatica")]);
                    item.CatePresu = DataUtil.DbValueToDefault<String>(datos[datos.GetOrdinal("CatePresu")]);
                    item.AgrupacionProgramaticaId = DataUtil.DbValueToDefault<Int32>(datos[datos.GetOrdinal("AgrupacionProgramaticaId")]);
                    item.ActProgra = DataUtil.DbValueToDefault<String>(datos[datos.GetOrdinal("ActProgra")]);
                    item.APEficacia = DataUtil.DbValueToDefault<Int32>(datos[datos.GetOrdinal("APEficacia")]);
                    item.APCateEficacia = DataUtil.DbValueToDefault<Int32>(datos[datos.GetOrdinal("APCateEficacia")]);
                    ls.Add(item);
                }
            }
            return ls;
        }

        public List<RepEficaMeta> ListaRepEficaMeta(int nPeriodo)
        {
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.uspRepEficaxMeta);
            oDatabase.AddInParameter(oDbCommand, "@nPeriodo", DbType.Int32, nPeriodo);

            List<RepEficaMeta> ls = new List<RepEficaMeta>();
            using (IDataReader datos = oDatabase.ExecuteReader(oDbCommand))
            {
                while (datos.Read())
                {
                    RepEficaMeta item = new RepEficaMeta();
                    item.NumeroDeMeta = DataUtil.DbValueToDefault<int>(datos[datos.GetOrdinal("NumeroDeMeta")]);
                    item.Nombre = DataUtil.DbValueToDefault<String>(datos[datos.GetOrdinal("Nombre")]);
                    item.Usuario = DataUtil.DbValueToDefault<String>(datos[datos.GetOrdinal("Usuario")]);
                    item.Eficacia = DataUtil.DbValueToDefault<Int32>(datos[datos.GetOrdinal("Eficacia")]);
                    item.Color = DataUtil.DbValueToDefault<String>(datos[datos.GetOrdinal("Color")]);
                    ls.Add(item);
                }
            }
            return ls;
        }

        public List<RepAvaFisiCatePre> ListaReporteAvaFisiCatePre(int nPeriodo)
        {
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.uspRepAvanceFisicoxCatPresu);
            oDatabase.AddInParameter(oDbCommand, "@nPeriodo", DbType.Int32, nPeriodo);

            List<RepAvaFisiCatePre> ls = new List<RepAvaFisiCatePre>();
            using (IDataReader datos = oDatabase.ExecuteReader(oDbCommand))
            {
                while (datos.Read())
                {
                    RepAvaFisiCatePre item = new RepAvaFisiCatePre();
                    item.CodigoAgrupacionProgramatica = DataUtil.DbValueToDefault<String>(datos[datos.GetOrdinal("CodigoAgrupacionProgramatica")]);
                    item.CatePresu = DataUtil.DbValueToDefault<String>(datos[datos.GetOrdinal("CatePresu")]);
                    item.AgrupacionProgramaticaId = DataUtil.DbValueToDefault<Int32>(datos[datos.GetOrdinal("AgrupacionProgramaticaId")]);
                    item.ActProgra = DataUtil.DbValueToDefault<String>(datos[datos.GetOrdinal("ActProgra")]);
                    item.APAvanceFisico = DataUtil.DbValueToDefault<Int32>(datos[datos.GetOrdinal("APAvanceFisico")]);
                    item.APCateAvanceFisico = DataUtil.DbValueToDefault<Int32>(datos[datos.GetOrdinal("APCateAvanceFisico")]);
                    ls.Add(item);
                }
            }
            return ls;
        }

        public List<RepAvaFisiMeta> ListaRepAvanceFisicoMeta(int nPeriodo)
        {
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.uspRepAvanceFisicoxMeta);
            oDatabase.AddInParameter(oDbCommand, "@nPeriodo", DbType.Int32, nPeriodo);

            List<RepAvaFisiMeta> ls = new List<RepAvaFisiMeta>();
            using (IDataReader datos = oDatabase.ExecuteReader(oDbCommand))
            {
                while (datos.Read())
                {
                    RepAvaFisiMeta item = new RepAvaFisiMeta();
                    item.NumeroDeMeta = DataUtil.DbValueToDefault<int>(datos[datos.GetOrdinal("NumeroDeMeta")]);
                    item.Nombre = DataUtil.DbValueToDefault<String>(datos[datos.GetOrdinal("Nombre")]);
                    item.Usuario = DataUtil.DbValueToDefault<String>(datos[datos.GetOrdinal("Usuario")]);
                    item.AvanceFisico = DataUtil.DbValueToDefault<Int32>(datos[datos.GetOrdinal("AvanceFisico")]);
                    item.Color = DataUtil.DbValueToDefault<String>(datos[datos.GetOrdinal("Color")]);
                    ls.Add(item);
                }
            }
            return ls;
        }

        public List<RepBusquedaActividaOpe> ListaRepBusquedaActividadOperativa(int nNumMeta, int nInstanciaId, string cLogro, int nPeriodo)
        {
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.uspRepBusquedaActividadOperativa);
            oDatabase.AddInParameter(oDbCommand, "@nNumMeta", DbType.Int32, nNumMeta);
            oDatabase.AddInParameter(oDbCommand, "@nInstanciaId", DbType.Int32, nInstanciaId);
            oDatabase.AddInParameter(oDbCommand, "@cLogro", DbType.String, cLogro);
            oDatabase.AddInParameter(oDbCommand, "@nPeriodo", DbType.Int32, nPeriodo);

            List<RepBusquedaActividaOpe> ls = new List<RepBusquedaActividaOpe>();
            using (IDataReader datos = oDatabase.ExecuteReader(oDbCommand))
            {
                while (datos.Read())
                {
                    RepBusquedaActividaOpe item = new RepBusquedaActividaOpe();
                    item.InstanciaId = DataUtil.DbValueToDefault<int>(datos[datos.GetOrdinal("InstanciaId")]);
                    item.cMeta = DataUtil.DbValueToDefault<string>(datos[datos.GetOrdinal("cMeta")]);
                    item.Nombre = DataUtil.DbValueToDefault<String>(datos[datos.GetOrdinal("Nombre")]);
                    item.cLogro1 = DataUtil.DbValueToDefault<String>(datos[datos.GetOrdinal("cLogro")]);
                    item.AvanceFisico = DataUtil.DbValueToDefault<int>(datos[datos.GetOrdinal("AvanceFisico")]);
                    item.Color = DataUtil.DbValueToDefault<string>(datos[datos.GetOrdinal("Color")]);
                    ls.Add(item);
                }
            }
            return ls;
        }

        public List<Combo> ListarMetas()
        {
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.uspListarMetas);
            List<Combo> ls = new List<Combo>();
            using (IDataReader datos = oDatabase.ExecuteReader(oDbCommand))
            {
                while (datos.Read())
                {
                    Combo item = new Combo();
                    item.CValue = DataUtil.DbValueToDefault<Int32>(datos[datos.GetOrdinal("ComboValue")]).ToString();
                    item.CText = DataUtil.DbValueToDefault<String>(datos[datos.GetOrdinal("ComboText")]);
                    ls.Add(item);
                }
            }
            return ls;
        }
        public List<Combo> ListarActividadOperativa(int nMetaId)
        {
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.uspListarActividadOperativa);
            oDatabase.AddInParameter(oDbCommand, "@nMetaId", DbType.Int32, nMetaId);
            List<Combo> ls = new List<Combo>();
            using (IDataReader datos = oDatabase.ExecuteReader(oDbCommand))
            {
                while (datos.Read())
                {
                    Combo item = new Combo();
                    item.CValue = DataUtil.DbValueToDefault<Int32>(datos[datos.GetOrdinal("ComboValue")]).ToString();
                    item.CText = DataUtil.DbValueToDefault<String>(datos[datos.GetOrdinal("ComboText")]);
                    ls.Add(item);
                }
            }
            return ls;
        }

        /*REPORTE DESEMPEÑO INDIVIDUAL*/
        public List<Combo> ListarJefe(int nUsuario)
        {
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.uspListarJefe);
            oDatabase.AddInParameter(oDbCommand, "@nUsuario", DbType.Int32, nUsuario);
            List<Combo> ls = new List<Combo>();
            using (IDataReader datos = oDatabase.ExecuteReader(oDbCommand))
            {
                while (datos.Read())
                {
                    Combo item = new Combo();
                    item.CValue = DataUtil.DbValueToDefault<Int32>(datos[datos.GetOrdinal("ComboValue")]).ToString();
                    item.CText = DataUtil.DbValueToDefault<String>(datos[datos.GetOrdinal("ComboText")]);
                    ls.Add(item);
                }
            }
            return ls;
        }

        public List<Combo> ListarColaboradores(int nJefeId, int nUsuarioId)
        {
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.uspListarColaboradores);
            oDatabase.AddInParameter(oDbCommand, "@nJefeId", DbType.Int32, nJefeId);
            oDatabase.AddInParameter(oDbCommand, "@nUsuario", DbType.Int32, nUsuarioId);
            List<Combo> ls = new List<Combo>();
            using (IDataReader datos = oDatabase.ExecuteReader(oDbCommand))
            {
                while (datos.Read())
                {
                    Combo item = new Combo();
                    item.CValue = DataUtil.DbValueToDefault<Int32>(datos[datos.GetOrdinal("ComboValue")]).ToString();
                    item.CText = DataUtil.DbValueToDefault<String>(datos[datos.GetOrdinal("ComboText")]);
                    ls.Add(item);
                }
            }
            return ls;
        }


        public List<RepDesempIndividual> ListaRepDesempIndividual(int nUsuarioId, int nPeriodo, int nPlanOpeId)
        {
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.uspRepDesempIndividual);
            oDatabase.AddInParameter(oDbCommand, "@nUsuarioId", DbType.Int32, nUsuarioId);
            oDatabase.AddInParameter(oDbCommand, "@nPeriodo", DbType.Int32, nPeriodo);
            oDatabase.AddInParameter(oDbCommand, "@nPlanOpeId", DbType.Int32, nPlanOpeId);

            List<RepDesempIndividual> ls = new List<RepDesempIndividual>();
            using (IDataReader datos = oDatabase.ExecuteReader(oDbCommand))
            {
                while (datos.Read())
                {
                    RepDesempIndividual item = new RepDesempIndividual();
                    item.NumeroDeMeta = DataUtil.DbValueToDefault<int>(datos[datos.GetOrdinal("NumeroDeMeta")]);
                    item.Nombre = DataUtil.DbValueToDefault<String>(datos[datos.GetOrdinal("Meta")]);
                    item.ActiviOpe = DataUtil.DbValueToDefault<String>(datos[datos.GetOrdinal("ActOpe")]);
                    item.UniMed = DataUtil.DbValueToDefault<String>(datos[datos.GetOrdinal("UniMed")]);
                    item.nEsperado = DataUtil.DbValueToDefault<Int32>(datos[datos.GetOrdinal("Esperado")]);
                    item.nLogrado = DataUtil.DbValueToDefault<Int32>(datos[datos.GetOrdinal("Logrado")]);
                    ls.Add(item);
                }
            }
            return ls;
        }

        public RepDesempIndiviValores OptenerDesemIndividualValores(int nJefeId ,int nColaboradorId, int nPeriodoId, int nPlanOpeId)
        {

            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.uspObtenerValoresRepDesempIndividual);
            oDatabase.AddInParameter(oDbCommand, "@nJefeId", DbType.Int32, nJefeId);
            oDatabase.AddInParameter(oDbCommand, "@nColaboradorId", DbType.Int32, nColaboradorId);
            oDatabase.AddInParameter(oDbCommand, "@nPeriodo", DbType.Int32, nPeriodoId);
            oDatabase.AddInParameter(oDbCommand, "@nPlanOpeId", DbType.Int32, nPlanOpeId);

            RepDesempIndiviValores oRepDesemInd = new RepDesempIndiviValores();

            using (IDataReader datos = oDatabase.ExecuteReader(oDbCommand))
            {
                while (datos.Read())
                {
                    oRepDesemInd.Efic_Ind = DataUtil.toDoubleDefault(datos[0].ToString());
                    oRepDesemInd.Avan_Ind = DataUtil.toDoubleDefault(datos[1].ToString());
                    oRepDesemInd.Efic_UO = DataUtil.toDoubleDefault(datos[2].ToString());
                    oRepDesemInd.Avan_UO = DataUtil.toDoubleDefault(datos[3].ToString());
                    oRepDesemInd.Efic_Inst = DataUtil.toDoubleDefault(datos[4].ToString());
                    oRepDesemInd.Avan_Inst = DataUtil.toDoubleDefault(datos[5].ToString());
                }
            }

            return oRepDesemInd;
        }

        /*END REPORTE DESEMPEÑO INDIVIDUAL*/

        /*Reporte AEAO*/

        public List<Combo> ListarAE()
        {
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.uspListarAE);
            List<Combo> ls = new List<Combo>();
            using (IDataReader datos = oDatabase.ExecuteReader(oDbCommand))
            {
                while (datos.Read())
                {
                    Combo item = new Combo();
                    item.CValue = DataUtil.DbValueToDefault<Int32>(datos[datos.GetOrdinal("ComboValue")]).ToString();
                    item.CText = DataUtil.DbValueToDefault<String>(datos[datos.GetOrdinal("ComboText")]);
                    ls.Add(item);
                }
            }
            return ls;
        }

        public List<Combo> ListarAExPlanOperativoId(Int32 PlanOperativoId)
        {
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.uspListarAExPlanOperativoId);
            oDatabase.AddInParameter(oDbCommand, "@nPlanOperativoId", DbType.Int32, PlanOperativoId);

            List<Combo> ls = new List<Combo>();
            using (IDataReader datos = oDatabase.ExecuteReader(oDbCommand))
            {
                while (datos.Read())
                {
                    Combo item = new Combo();
                    item.CValue = DataUtil.DbValueToDefault<Int32>(datos[datos.GetOrdinal("ComboValue")]).ToString();
                    item.CText = DataUtil.DbValueToDefault<String>(datos[datos.GetOrdinal("ComboText")]);
                    ls.Add(item);
                }
            }
            return ls;
        }

        public List<Combo> ListarPeriodoCalexPlanOperativoId(Int32 PlanOperativoId)
        {
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.uspListarPeriodoCalendarioxPlanOperativo);
            oDatabase.AddInParameter(oDbCommand, "@nPlanOperativoId", DbType.Int32, PlanOperativoId);

            List<Combo> ls = new List<Combo>();
            using (IDataReader datos = oDatabase.ExecuteReader(oDbCommand))
            {
                while (datos.Read())
                {
                    Combo item = new Combo();
                    item.CValue = DataUtil.DbValueToDefault<Byte>(datos[datos.GetOrdinal("ComboValue")]).ToString();
                    item.CText = DataUtil.DbValueToDefault<String>(datos[datos.GetOrdinal("ComboText")]);
                    ls.Add(item);
                }
            }
            return ls;
        }

        public List<ReporteAEAO> ListaRepAEAO(int nAEId, int nPeriodo,int nPlanOperativoId)
        {
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimiento.uspRepAEAO);
            oDatabase.AddInParameter(oDbCommand, "@nAEId", DbType.Int32, nAEId);
            oDatabase.AddInParameter(oDbCommand, "@nPeriodo", DbType.Int32, nPeriodo);
            oDatabase.AddInParameter(oDbCommand, "@nPlanOperativoId", DbType.Int32, nPlanOperativoId);

            List<ReporteAEAO> ls = new List<ReporteAEAO>();
            using (IDataReader datos = oDatabase.ExecuteReader(oDbCommand))
            {
                while (datos.Read())
                {
                    ReporteAEAO item = new ReporteAEAO();
                    item.cAENombre = DataUtil.DbValueToDefault<String>(datos[datos.GetOrdinal("AENombre")]);
                    item.cActividadOpe = DataUtil.DbValueToDefault<String>(datos[datos.GetOrdinal("ActiviOpe")]);
                    item.cLogro1 = DataUtil.DbValueToDefault<String>(datos[datos.GetOrdinal("cLogro")]);
                    ls.Add(item);
                }
            }
            return ls;
        }

        /*END Reporte AEAO*/



    }
}
