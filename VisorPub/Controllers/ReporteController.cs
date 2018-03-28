using System;
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
            AvanceInformeAD handlerAvance = new AvanceInformeAD();
            ReporteViewModel modelo = new ReporteViewModel();
            modelo.lsPOIs = handlerAvance.ListaPOIVigentesCombo();
            return View(modelo);
        }

        [RequiresAuthenticationAttribute]
        public JsonResult ListarPeriodoCalendario(Int32 PlanOperativoId)
        {
            ReporteViewModel modelo = new ReporteViewModel();
            modelo.lsPeriodoCale = oReporteAD.ListarPeriodoCalexPlanOperativoId(PlanOperativoId);
            return Json(JsonConvert.SerializeObject(modelo));
        }

        [RequiresAuthenticationAttribute]
        public JsonResult ReporteEficaciaCatePre(int nPeriodo, int nPlanOpeId)
        {
            List<ReporteEfiCatePre> lsReporte = oReporteAD.ListaReporteEfiCatePre(nPeriodo, nPlanOpeId);
            return Json(JsonConvert.SerializeObject(lsReporte));
        }


        [RequiresAuthenticationAttribute]
        [ActionName("Reporte-Eficacia-Meta")]
        public ActionResult ReporteEficaciaMeta()
        {
            AvanceInformeAD handlerAvance = new AvanceInformeAD();
            ReporteViewModel modelo = new ReporteViewModel();
            modelo.lsPOIs = handlerAvance.ListaPOIVigentesCombo();
            return View(modelo);
        }

        [RequiresAuthenticationAttribute]
        public JsonResult ReporteEficaciaMetaLista(int nPeriodo, int nPlanOpeId)
        {
            List<RepEficaMeta> lsReporte = oReporteAD.ListaRepEficaMeta(nPeriodo, nPlanOpeId);
            return Json(JsonConvert.SerializeObject(lsReporte));
        }

        /******************************************/

        [RequiresAuthenticationAttribute]
        [ActionName("Reporte-Avance-Fisico-Anual-CatePresu")]
        public ActionResult ReporteAvanceFiscoCatePresu()
        {
            AvanceInformeAD handlerAvance = new AvanceInformeAD();
            ReporteViewModel modelo = new ReporteViewModel();
            modelo.lsPOIs = handlerAvance.ListaPOIVigentesCombo();
            return View(modelo);
        }

        [RequiresAuthenticationAttribute]
        public JsonResult ListarReporteAvanceFisicoCatePresu(int nPeriodo, int nPlanOpeId)
        {
            List<RepAvaFisiCatePre> lsReporte = oReporteAD.ListaReporteAvaFisiCatePre(nPeriodo, nPlanOpeId);
            return Json(JsonConvert.SerializeObject(lsReporte));
        }

        [RequiresAuthenticationAttribute]
        [ActionName("Reporte-Avance-Fisico-Anual-Meta")]
        public ActionResult ReporteAvanceFisicoMeta()
        {
            AvanceInformeAD handlerAvance = new AvanceInformeAD();
            ReporteViewModel modelo = new ReporteViewModel();
            modelo.lsPOIs = handlerAvance.ListaPOIVigentesCombo();
            return View(modelo);
        }

        [RequiresAuthenticationAttribute]
        public JsonResult ListarReporteAvanceFisicoMeta(int nPeriodo, int nPlanOpeId)
        {
            List<RepAvaFisiMeta> lsReporte = oReporteAD.ListaRepAvanceFisicoMeta(nPeriodo, nPlanOpeId);
            return Json(JsonConvert.SerializeObject(lsReporte));
        }



        [RequiresAuthenticationAttribute]
        [ActionName("Reporte-Logros-Actividad-Operativa")]
        public ActionResult ReporteActividadOperativa()
        {
            AvanceInformeAD handlerAvance = new AvanceInformeAD();
            ReporteViewModel modelo = new ReporteViewModel();
            //modelo.lsMetas = oReporteAD.ListarMetas();
            modelo.lsPOIs = handlerAvance.ListaPOIVigentesCombo();
            return View(modelo);
        }

        [RequiresAuthenticationAttribute]
        public JsonResult ListarLogrosComboxPlanOperativoId(Int32 PlanOperativoId)
        {
            ReporteViewModel modelo = new ReporteViewModel();

            modelo.lsMetas = oReporteAD.ListarMetas(PlanOperativoId);
            modelo.lsPeriodoCale = oReporteAD.ListarPeriodoCalexPlanOperativoId(PlanOperativoId);
            return Json(JsonConvert.SerializeObject(modelo));
        }


        [RequiresAuthenticationAttribute]
        public JsonResult ListaRepBusquedaActividadOperativa(int nNumMeta, int nInstanciaId, string cLogro, int nPeriodo, int nPlanOpeId)
        {
            List<RepBusquedaActividaOpe> lsReporte = oReporteAD.ListaRepBusquedaActividadOperativa(nNumMeta, nInstanciaId, cLogro, nPeriodo, nPlanOpeId);
            return Json(JsonConvert.SerializeObject(lsReporte));
        }

        //public JsonResult ListarMetas()
        //{
        //    List<Combo> lsMetas = oReporteAD.ListarMetas();
        //    return Json(JsonConvert.SerializeObject(lsMetas));
        //}

        public JsonResult ListarActividadOperativa(int nMetaId, int nPlanOpeId)
        {
            List<Combo> lsActOpe = oReporteAD.ListarActividadOperativa(nMetaId, nPlanOpeId);
            return Json(JsonConvert.SerializeObject(lsActOpe));
        }


        //Reporte de Evaluación Personal

        //[RequiresAuthenticationAttribute]
        [ActionName("Reporte-Desempeño-Individual")]
        public ActionResult ReporteDesempenoIndividual()
        {
            AvanceInformeAD handlerAvance = new AvanceInformeAD();
            ReporteViewModel modelo = new ReporteViewModel();
            modelo.lsPOIs = handlerAvance.ListaPOIVigentesCombo();
            return View(modelo);

        }

        public JsonResult ListarCombosDesemIndixPlanOperativoId(Int32 PlanOperativoId)
        {
            ReporteViewModel modelo = new ReporteViewModel();
            modelo.lsPeriodoCale = oReporteAD.ListarPeriodoCalexPlanOperativoId(PlanOperativoId);
            return Json(JsonConvert.SerializeObject(modelo));
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
        public JsonResult ReporteDesempIndividual(int nUsuarioId, int nPeriodo, int nPlanOpeId)
        {
            List<RepDesempIndividual> lsReporte = oReporteAD.ListaRepDesempIndividual(nUsuarioId, nPeriodo, nPlanOpeId);
            return Json(JsonConvert.SerializeObject(lsReporte));
        }

        public JsonResult OptenerDesemIndividualValores(int nJefeId, int nColaboradorId, int nPeriodoId, int nPlanOpeId)
        {
            RepDesempIndiviValores oRepDem = oReporteAD.OptenerDesemIndividualValores(nJefeId, nColaboradorId, nPeriodoId, nPlanOpeId);
            return Json(JsonConvert.SerializeObject(oRepDem));
        }


        /*REPORTE AEAO*/

        [RequiresAuthenticationAttribute]
        [ActionName("Reporte-AE-AO")]
        public ActionResult ReporteAEAO()
        {
            //ReporteViewModel modelo = new ReporteViewModel();
            //modelo.lsAcEst = oReporteAD.ListarAE();
            
            AvanceInformeAD handlerAvance = new AvanceInformeAD();
            ReporteViewModel modelo = new ReporteViewModel();
            modelo.lsPOIs = handlerAvance.ListaPOIVigentesCombo();
            return View(modelo);

        }

        [RequiresAuthenticationAttribute]
        public JsonResult ListarAExPlanOperativoId(Int32 PlanOperativoId)
        {
            ReporteViewModel modelo = new ReporteViewModel();

            modelo.lsAcEst = oReporteAD.ListarAExPlanOperativoId(PlanOperativoId);
            modelo.lsPeriodoCale = oReporteAD.ListarPeriodoCalexPlanOperativoId(PlanOperativoId);
            return Json(JsonConvert.SerializeObject(modelo));
        }

        [RequiresAuthenticationAttribute]
        public JsonResult ListaRepActividadOpe(int nAEId, int nPeriodo, int nPlanOperativoId)
        {
            List<ReporteAEAO> lsReporte = oReporteAD.ListaRepAEAO(nAEId, nPeriodo, nPlanOperativoId);
            return Json(JsonConvert.SerializeObject(lsReporte));
        }


        /*END REPORTE AEAO*/


        /*Reporte Grafico*/


        [RequiresAuthenticationAttribute]
        [ActionName("ReporteGrafico")]
        public ActionResult ReporteGrafico()
        {
            AvanceInformeAD handlerAvance = new AvanceInformeAD();
            ReporteViewModel modelo = new ReporteViewModel();
            modelo.lsPOIs = handlerAvance.ListaPOIVigentesCombo();
            return View(modelo);
        }




        //Excel Reports

        public void GetExcelReporteEficaciaMeta(int PlanOperativoId, int nPeriodo, string cPlan, string cPeriodo)
        {
            List<RepEficaMeta> oMatriz = new List<RepEficaMeta>();
            oMatriz = oReporteAD.ListaRepEficaMeta(nPeriodo, PlanOperativoId);

            string cHoy = "";
            DateTime Hoy = DateTime.Today;

            cHoy = (Hoy).ToString("ddMMyyyy");

            using (var excelPackage = new ExcelPackage())
            {
                excelPackage.Workbook.Properties.Author = "IIAP";
                excelPackage.Workbook.Properties.Title = "ReporteEficacia";
                var sheet = excelPackage.Workbook.Worksheets.Add(cPeriodo + "_" + cPlan);

                // output a line for the headers

                //CreateHeader(sheet);

                sheet.Cells["A1"].Value = "N° META";
                sheet.Cells["B1"].Value = "NOMBRE";
                sheet.Cells["C1"].Value = "RESPONSABLE";
                sheet.Cells["D1"].Value = "EFICACIA";


                using (ExcelRange rng = sheet.Cells["A1:D1"])
                {
                    rng.Style.Font.Bold = true;
                    rng.Style.Fill.PatternType = ExcelFillStyle.Solid;                      //Set Pattern for the background to Solid
                    rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(242, 242, 242));  //Set color to dark blue
                    rng.Style.Font.Color.SetColor(Color.Black);
                    rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    rng.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                }

                var rowIndex = 2;
                    foreach (var item in oMatriz)
                    {
                        var col = 1;

                        //if (item.Nivel == 1)
                        //{
                        //    var cell = sheet.Cells[rowIndex, 1];
                        //    cell.Style.Font.Bold = true;
                        //}

                        sheet.Cells[rowIndex, col++].Value = item.NumeroDeMeta;
                        sheet.Cells[rowIndex, col++].Value = item.Nombre;
                        sheet.Cells[rowIndex, col++].Value = item.Usuario;
                        sheet.Cells[rowIndex, col++].Value = item.Eficacia.ToString() + " %";

                        var cell = sheet.Cells[rowIndex, 4];
                        cell.Style.Fill.PatternType = ExcelFillStyle.Solid;

                        switch (item.Color)
                        {
                            case "Azul":
                                cell.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(93, 93, 255));
                                break;
                            case "Verde":
                                cell.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(21, 171, 21));
                                break;
                            case "Amarillo":
                                cell.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 255, 81));
                                break;
                            case "Rojo":
                                cell.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(253, 41, 41));
                                break;
                        }

                    rowIndex++;
                    }

                using (ExcelRange rng = sheet.Cells["A1:D" + (rowIndex - 1).ToString()])
                {
                    rng.Style.Border.Top.Style = rng.Style.Border.Left.Style = rng.Style.Border.Bottom.Style = rng.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                }

                //using (ExcelRange rng = sheet.Cells[(PlanOperativoId == 14 ? "I1:I" : "F1:F") + (rowIndex - 1).ToString()])
                //{
                //    rng.Style.Font.Bold = true;
                //}

                //using (ExcelRange col = sheet.Cells[2, 7, 1 + oMatriz.Count, 7])
                //{
                //    col.Style.Fill.PatternType = ExcelFillStyle.Solid;
                //    col.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(140, 173, 232));
                //}


                using (ExcelRange col = sheet.Cells[1, 1, 1 + oMatriz.Count, 1])
                {
                    col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    col.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                }

                using (ExcelRange col = sheet.Cells[1, 3, 1 + oMatriz.Count, 4])
                {
                    col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    col.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                }

                sheet.Column(1).Width = 10;
                    sheet.Column(2).Width = 106;
                    sheet.Column(3).Width = 16;
                    sheet.Column(4).Width = 12;


                using (ExcelRange col = sheet.Cells[1, 1, 1 + oMatriz.Count, 4])
                {
                    col.Style.WrapText = true;
                    col.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                }

                Response.ClearContent();
                Response.BinaryWrite(excelPackage.GetAsByteArray());
                Response.AddHeader("content-disposition",
                          "attachment;filename=ReporteEficaciaMeta" + cPlan + "_" + cPeriodo + "_" + cHoy + ".xlsx");
                Response.ContentType = "application/excel";
                Response.Flush();
                Response.End();
            }
        }

        public void GetExcelReporteAvanceFisicoAnualMeta(int PlanOperativoId, int nPeriodo, string cPlan, string cPeriodo)
        {
            List<RepAvaFisiMeta> oMatriz = new List<RepAvaFisiMeta>();
            oMatriz = oReporteAD.ListaRepAvanceFisicoMeta(nPeriodo, PlanOperativoId);

            string cHoy = "";
            DateTime Hoy = DateTime.Today;

            cHoy = (Hoy).ToString("ddMMyyyy");

            using (var excelPackage = new ExcelPackage())
            {
                excelPackage.Workbook.Properties.Author = "IIAP";
                excelPackage.Workbook.Properties.Title = "ReporteAvanceFisicoMeta";
                var sheet = excelPackage.Workbook.Worksheets.Add(cPeriodo + "_" + cPlan);


                sheet.Cells["A1"].Value = "N° META";
                sheet.Cells["B1"].Value = "NOMBRE";
                sheet.Cells["C1"].Value = "RESPONSABLE";
                sheet.Cells["D1"].Value = "AVANCE FÍSICO";


                using (ExcelRange rng = sheet.Cells["A1:D1"])
                {
                    rng.Style.Font.Bold = true;
                    rng.Style.Fill.PatternType = ExcelFillStyle.Solid;                      //Set Pattern for the background to Solid
                    rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(242, 242, 242));  //Set color to dark blue
                    rng.Style.Font.Color.SetColor(Color.Black);
                    rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    rng.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                }

                var rowIndex = 2;
                foreach (var item in oMatriz)
                {
                    var col = 1;

                    sheet.Cells[rowIndex, col++].Value = item.NumeroDeMeta;
                    sheet.Cells[rowIndex, col++].Value = item.Nombre;
                    sheet.Cells[rowIndex, col++].Value = item.Usuario;
                    sheet.Cells[rowIndex, col++].Value = item.AvanceFisico.ToString() + " %";

                    var cell = sheet.Cells[rowIndex, 4];
                    cell.Style.Fill.PatternType = ExcelFillStyle.Solid;

                    switch (item.Color)
                    {
                        case "Azul":
                            cell.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(93, 93, 255));
                            break;
                        case "Verde":
                            cell.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(21, 171, 21));
                            break;
                        case "Amarillo":
                            cell.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 255, 81));
                            break;
                        case "Rojo":
                            cell.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(253, 41, 41));
                            break;
                    }

                    rowIndex++;
                }

                using (ExcelRange rng = sheet.Cells["A1:D" + (rowIndex - 1).ToString()])
                {
                    rng.Style.Border.Top.Style = rng.Style.Border.Left.Style = rng.Style.Border.Bottom.Style = rng.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                }

                using (ExcelRange col = sheet.Cells[1, 1, 1 + oMatriz.Count, 1])
                {
                    col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    col.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                }

                using (ExcelRange col = sheet.Cells[1, 3, 1 + oMatriz.Count, 4])
                {
                    col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    col.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                }

                sheet.Column(1).Width = 10;
                sheet.Column(2).Width = 106;
                sheet.Column(3).Width = 16;
                sheet.Column(4).Width = 12;


                using (ExcelRange col = sheet.Cells[1, 1, 1 + oMatriz.Count, 4])
                {
                    col.Style.WrapText = true;
                    col.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                }

                Response.ClearContent();
                Response.BinaryWrite(excelPackage.GetAsByteArray());
                Response.AddHeader("content-disposition",
                          "attachment;filename=ReporteAvanceFisicoMeta" + cPlan + "_" + cPeriodo + "_" + cHoy + ".xlsx");
                Response.ContentType = "application/excel";
                Response.Flush();
                Response.End();
            }
        }

        public void GetExcelReporteLogrosActividadOperativa(int nNumMeta, int nInstanciaId, string cLogro, int nPeriodo, int nPlanOpeId, string cPlan, string cPeriodo)
        {
            List<RepBusquedaActividaOpe> oMatriz = new List<RepBusquedaActividaOpe>();
            oMatriz = oReporteAD.ListaRepBusquedaActividadOperativa(nNumMeta, nInstanciaId, cLogro, nPeriodo, nPlanOpeId);

            string cHoy = "";
            DateTime Hoy = DateTime.Today;

            cHoy = (Hoy).ToString("ddMMyyyy");

            using (var excelPackage = new ExcelPackage())
            {
                excelPackage.Workbook.Properties.Author = "IIAP";
                excelPackage.Workbook.Properties.Title = "ReporteLogrosActividadOperativa";
                var sheet = excelPackage.Workbook.Worksheets.Add(cPeriodo + "_" + cPlan);


                sheet.Cells["A1"].Value = "N° META";
                sheet.Cells["B1"].Value = "ACTIVIDAD OPERATIVA";
                sheet.Cells["C1"].Value = "LOGRO";
                sheet.Cells["D1"].Value = "AVANCE FÍSICO";


                using (ExcelRange rng = sheet.Cells["A1:D1"])
                {
                    rng.Style.Font.Bold = true;
                    rng.Style.Fill.PatternType = ExcelFillStyle.Solid;                      //Set Pattern for the background to Solid
                    rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(242, 242, 242));  //Set color to dark blue
                    rng.Style.Font.Color.SetColor(Color.Black);
                    rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    rng.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                }

                var rowIndex = 2;
                foreach (var item in oMatriz)
                {
                    var col = 1;

                    sheet.Cells[rowIndex, col++].Value = item.cMeta;
                    sheet.Cells[rowIndex, col++].Value = item.Nombre;
                    sheet.Cells[rowIndex, col++].Value = item.cLogro1;
                    sheet.Cells[rowIndex, col++].Value = item.AvanceFisico.ToString() + " %";

                    var cell = sheet.Cells[rowIndex, 4];
                    cell.Style.Fill.PatternType = ExcelFillStyle.Solid;

                    switch (item.Color)
                    {
                        case "Azul":
                            cell.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(93, 93, 255));
                            break;
                        case "Verde":
                            cell.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(21, 171, 21));
                            break;
                        case "Amarillo":
                            cell.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 255, 81));
                            break;
                        case "Rojo":
                            cell.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(253, 41, 41));
                            break;
                    }

                    rowIndex++;
                }

                using (ExcelRange rng = sheet.Cells["A1:D" + (rowIndex - 1).ToString()])
                {
                    rng.Style.Border.Top.Style = rng.Style.Border.Left.Style = rng.Style.Border.Bottom.Style = rng.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                }

                using (ExcelRange col = sheet.Cells[2, 4, 1 + oMatriz.Count, 4])
                {
                    col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    col.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                }

                sheet.Column(1).Width = 42;
                sheet.Column(2).Width = 42;
                sheet.Column(3).Width = 106;
                sheet.Column(4).Width = 15;


                using (ExcelRange col = sheet.Cells[1, 1, 1 + oMatriz.Count, 4])
                {
                    col.Style.WrapText = true;
                    col.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                }

                Response.ClearContent();
                Response.BinaryWrite(excelPackage.GetAsByteArray());
                Response.AddHeader("content-disposition",
                          "attachment;filename=ReporteLogrosActividadesOperativas" + cPlan + "_" + cPeriodo + "_" + cHoy + ".xlsx");
                Response.ContentType = "application/excel";
                Response.Flush();
                Response.End();
            }
        }


        public void GetExcelReporteAEAO(int nAEId, int nPlanOpeId, int nPeriodo, string cPlan, string cPeriodo)
        {
            List<ReporteAEAO> oMatriz = new List<ReporteAEAO>();
            oMatriz = oReporteAD.ListaRepAEAO(nAEId, nPeriodo, nPlanOpeId);

            string cHoy = "";
            DateTime Hoy = DateTime.Today;

            cHoy = (Hoy).ToString("ddMMyyyy");

            using (var excelPackage = new ExcelPackage())
            {
                excelPackage.Workbook.Properties.Author = "IIAP";
                excelPackage.Workbook.Properties.Title = "ReporteAEAO";
                var sheet = excelPackage.Workbook.Worksheets.Add(cPeriodo + "_" + cPlan);


                sheet.Cells["A1"].Value = "ACCIÓN ESTRATÉGICA";
                sheet.Cells["B1"].Value = "ACTIVIDAD OPERATIVA";
                sheet.Cells["C1"].Value = "LOGRO";


                using (ExcelRange rng = sheet.Cells["A1:C1"])
                {
                    rng.Style.Font.Bold = true;
                    rng.Style.Fill.PatternType = ExcelFillStyle.Solid;                      //Set Pattern for the background to Solid
                    rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(242, 242, 242));  //Set color to dark blue
                    rng.Style.Font.Color.SetColor(Color.Black);
                    rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    rng.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                }

                var rowIndex = 2;
                foreach (var item in oMatriz)
                {
                    var col = 1;

                    sheet.Cells[rowIndex, col++].Value = item.cAENombre;
                    sheet.Cells[rowIndex, col++].Value = item.cActividadOpe;
                    sheet.Cells[rowIndex, col++].Value = item.cLogro1;

                    rowIndex++;
                }

                using (ExcelRange rng = sheet.Cells["A1:C" + (rowIndex - 1).ToString()])
                {
                    rng.Style.Border.Top.Style = rng.Style.Border.Left.Style = rng.Style.Border.Bottom.Style = rng.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                }

                sheet.Column(1).Width = 42;
                sheet.Column(2).Width = 42;
                sheet.Column(3).Width = 106;
                sheet.Column(4).Width = 15;


                using (ExcelRange col = sheet.Cells[1, 1, 1 + oMatriz.Count, 4])
                {
                    col.Style.WrapText = true;
                    col.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                }

                Response.ClearContent();
                Response.BinaryWrite(excelPackage.GetAsByteArray());
                Response.AddHeader("content-disposition",
                          "attachment;filename=ReporteAEAO" + cPlan + "_" + cPeriodo + "_" + cHoy + ".xlsx");
                Response.ContentType = "application/excel";
                Response.Flush();
                Response.End();
            }
        }


    }
}