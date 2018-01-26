﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Seguridad.filters;
using VisorPub.Models;
using ADSql;
using ESql;
using Newtonsoft.Json;

using Excel = Microsoft.Office.Interop.Excel;

using System.Data;
using System.Text;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Style;
using System.Drawing;
using VisorPub.Controllers.Base;


namespace VisorPub.Controllers
{
    public class ReporteController : Controller
    {

        ReportesAD oReporteAD = new ReportesAD();


        [RequiresAuthenticationAttribute]
        public ActionResult Index()
        {
            return View();
        }

        [RequiresAuthenticationAttribute]
        [ActionName("Reporte-Eficacia-ActProgra-CatePresu")]
        public ActionResult ReporteEficacia()
        {
            return View();
        }

        [RequiresAuthenticationAttribute]
        public JsonResult ReporteEficaciaCatePre(int nPeriodo)
        {
            List<ReporteEfiCatePre> lsReporte = oReporteAD.ListaReporteEfiCatePre(nPeriodo);
            return Json(JsonConvert.SerializeObject(lsReporte));
        }


        [RequiresAuthenticationAttribute]
        [ActionName("Reporte-Eficacia-Meta")]
        public ActionResult ReporteEficaciaMeta()
        {
            return View();
        }

        [RequiresAuthenticationAttribute]
        public JsonResult ReporteEficaciaMetaLista(int nPeriodo)
        {
            List<RepEficaMeta> lsReporte = oReporteAD.ListaRepEficaMeta(nPeriodo);
            return Json(JsonConvert.SerializeObject(lsReporte));
        }

        /******************************************/

        [RequiresAuthenticationAttribute]
        [ActionName("Reporte-Avance-Fisico-Anual-CatePresu")]
        public ActionResult ReporteAvanceFiscoCatePresu()
        {
            return View();
        }

        [RequiresAuthenticationAttribute]
        public JsonResult ListarReporteAvanceFisicoCatePresu(int nPeriodo)
        {
            List<RepAvaFisiCatePre> lsReporte = oReporteAD.ListaReporteAvaFisiCatePre(nPeriodo);
            return Json(JsonConvert.SerializeObject(lsReporte));
        }

        [RequiresAuthenticationAttribute]
        [ActionName("Reporte-Avance-Fisico-Anual-Meta")]
        public ActionResult ReporteAvanceFisicoMeta()
        {
            return View();
        }

        [RequiresAuthenticationAttribute]
        public JsonResult ListarReporteAvanceFisicoMeta(int nPeriodo)
        {
            List<RepAvaFisiMeta> lsReporte = oReporteAD.ListaRepAvanceFisicoMeta(nPeriodo);
            return Json(JsonConvert.SerializeObject(lsReporte));
        }



        [RequiresAuthenticationAttribute]
        [ActionName("Reporte-Logros-Actividad-Operativa")]
        public ActionResult ReporteActividadOperativa()
        {
            ReporteViewModel modelo = new ReporteViewModel();
            modelo.lsMetas = oReporteAD.ListarMetas();
            return View(modelo);
        }


        [RequiresAuthenticationAttribute]
        public JsonResult ListaRepBusquedaActividadOperativa(int nNumMeta, int nInstanciaId, string cLogro, int nPeriodo)
        {
            List<RepBusquedaActividaOpe> lsReporte = oReporteAD.ListaRepBusquedaActividadOperativa(nNumMeta, nInstanciaId, cLogro, nPeriodo);
            return Json(JsonConvert.SerializeObject(lsReporte));
        }

        //public JsonResult ListarMetas()
        //{
        //    List<Combo> lsMetas = oReporteAD.ListarMetas();
        //    return Json(JsonConvert.SerializeObject(lsMetas));
        //}

        public JsonResult ListarActividadOperativa(int nMetaId)
        {
            List<Combo> lsActOpe = oReporteAD.ListarActividadOperativa(nMetaId);
            return Json(JsonConvert.SerializeObject(lsActOpe));
        }


        //Reporte de Evaluación Personal

        //[RequiresAuthenticationAttribute]
        [ActionName("Reporte-Desempeño-Individual")]
        public ActionResult ReporteDesempenoIndividual()
        {
            ReporteViewModel modelo = new ReporteViewModel();
            //Usuario oUsuario = new Usuario();
            //oUsuario = (Usuario)Session["Datos"];
            //modelo.lsJefes = oReporteAD.ListarJefe(oUsuario.UserName);
            return View(modelo);
        }


        public JsonResult ListarJefe(int nJefeId)
        {
            Usuario oUsuario = new Usuario();
            oUsuario = (Usuario)Session["Datos"];

            if (oUsuario != null)
            {
                if (oUsuario.UserId != 0)
                {
                    nJefeId = oUsuario.UserId;
                }
            }

            List<Combo> lsJefes = oReporteAD.ListarJefe(nJefeId);
            return Json(JsonConvert.SerializeObject(lsJefes));
        }

        public JsonResult ListarColaboradores(int nJefeId, int nUsuarioId)
        {
            Usuario oUsuario = new Usuario();
            oUsuario = (Usuario)Session["Datos"];

            if (oUsuario != null)
            {
                if (oUsuario.UserId == nJefeId || oUsuario.UserId == 37 || oUsuario.UserId == 133)
                {
                    nUsuarioId = -1;
                }
            }

            List<Combo> lsColabo = oReporteAD.ListarColaboradores(nJefeId, nUsuarioId);
            return Json(JsonConvert.SerializeObject(lsColabo)); 
        }

        //[RequiresAuthenticationAttribute]
        public JsonResult ReporteDesempIndividual(int nUsuarioId, int nPeriodo)
        {
            List<RepDesempIndividual> lsReporte = oReporteAD.ListaRepDesempIndividual(nUsuarioId, nPeriodo);
            return Json(JsonConvert.SerializeObject(lsReporte));
        }

        public JsonResult OptenerDesemIndividualValores(int nJefeId, int nColaboradorId, int nPeriodoId)
        {
            RepDesempIndiviValores oRepDem = oReporteAD.OptenerDesemIndividualValores(nJefeId, nColaboradorId, nPeriodoId);
            return Json(JsonConvert.SerializeObject(oRepDem));
        }


        /*REPORTE AEAO*/

        [RequiresAuthenticationAttribute]
        [ActionName("Reporte-AE-AO")]
        public ActionResult ReporteAEAO()
        {
            ReporteViewModel modelo = new ReporteViewModel();
            modelo.lsAcEst = oReporteAD.ListarAE();
            return View(modelo);
        }

        [RequiresAuthenticationAttribute]
        public JsonResult ListaRepActividadOpe(int nAEId, int nPeriodo)
        {
            List<ReporteAEAO> lsReporte = oReporteAD.ListaRepAEAO(nAEId, nPeriodo);
            return Json(JsonConvert.SerializeObject(lsReporte));
        }


        /*END REPORTE AEAO*/

    }
}