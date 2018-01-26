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
    public class InicioController : Controller
    {

        public class itemAvance
        {
            public int PlanOperativoId;
            public int nPeriodo;
        }


        AvanceInformeAD handlerAvance = new AvanceInformeAD();
        [RequiresAuthenticationAttribute]
        public ActionResult Index()
        {
            return View();
        }
        [RequiresAuthenticationAttribute]
        public ActionResult RegistrarAvance()
        {
            RegistrarAvanceViewModel modelo=new RegistrarAvanceViewModel();
            Usuario oUsuario = new Usuario();
            oUsuario = (Usuario)Session["Datos"];
            modelo.lsPOIsVigentes = handlerAvance.ListaPOIVigentesCombo();
            modelo.lsMotivoRetraso = handlerAvance.ListaMotivosDeRestraso();
            modelo.idUser = oUsuario.UserId;
            return View(modelo);
        }


        //[RequiresAuthenticationAttribute]
        //public ActionResult ReporteEficacia()
        //{
        //    return View();
        //}

        //[RequiresAuthenticationAttribute]
        //public JsonResult ReporteEficacia_I_S()
        //{
        //    List<ReporteEfiCatePre> lsReporte= handlerAvance.ListaReporteEfiCatePre_I_S();
        //    return Json(JsonConvert.SerializeObject(lsReporte));
        //}

        //[RequiresAuthenticationAttribute]
        public JsonResult ListarMetasDeResponsableMeta(Int32 PlanOperativoId, Int32 ResponsableMeta)
        {
            List<Combo> lsMetas = handlerAvance.ListarMetasDeResponsableMeta(PlanOperativoId, ResponsableMeta);
            return Json(JsonConvert.SerializeObject(lsMetas));
        }

        

        [RequiresAuthenticationAttribute]
        public JsonResult ListarMatrizPogramacion(Int32 InstanciaId, Int32 PlanOperativoId, Int32 nPeriodo, Int32 nRespId)
        {
            itemAvance itemAvan = new itemAvance();
            itemAvan.PlanOperativoId = PlanOperativoId;
            itemAvan.nPeriodo = nPeriodo;
            Session["itemAvance"] = itemAvan;
            List<MatrizProgramacionFisica> lsMatriz = handlerAvance.ListaMatrizDeProgramacionFisica(InstanciaId, nRespId);
            //lsMatriz = handlerAvance.ordenarMatrizPorPadre(lsMatriz);
            return Json(JsonConvert.SerializeObject(lsMatriz));
        }

       public void GetExcelMatrizProgramacion(int InstanciaId, int PlanOperativoId, int nPeriodo, string cPlanText, string cPeriodo, int nRespId)
        {
            List<MatrizProgramacionFisica> oMatriz = new List<MatrizProgramacionFisica>();
            oMatriz = handlerAvance.ListaMatrizDeProgramacionFisica(InstanciaId, nRespId);

            string cHoy = "";
            DateTime Hoy = DateTime.Today;

            cHoy = (Hoy).ToString("ddMMyyyy");

            using (var excelPackage = new ExcelPackage())
            {
                excelPackage.Workbook.Properties.Author = "IIAP";
                excelPackage.Workbook.Properties.Title = "Reporte";
                var sheet = excelPackage.Workbook.Worksheets.Add("Rep_Matriz");

                // output a line for the headers

                //CreateHeader(sheet);

                sheet.Cells["A1"].Value = "Actividad Operativa/Tarea";
                sheet.Cells["B1"].Value = "Unidad de Medida";

                if (nPeriodo == 1)
                {
                    sheet.Cells["C1"].Value = "Ene";
                    sheet.Cells["D1"].Value = "Feb";
                    sheet.Cells["E1"].Value = "Mar";
                    sheet.Cells["F1"].Value = "Abr";
                    sheet.Cells["G1"].Value = "May";
                    sheet.Cells["H1"].Value = "Jun";
                    sheet.Cells["I1"].Value = "Avance Programado";
                    sheet.Cells["J1"].Value = "Avance I Sem.";
                    sheet.Cells["K1"].Value = "Motivo del Retraso";
                    sheet.Cells["L1"].Value = "Logros más importantes";

                    using (ExcelRange rng = sheet.Cells["A1:L1"])
                    {
                        rng.Style.Font.Bold = true;
                        rng.Style.Fill.PatternType = ExcelFillStyle.Solid;                      //Set Pattern for the background to Solid
                        rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(242, 242, 242));  //Set color to dark blue
                        rng.Style.Font.Color.SetColor(Color.Black);
                        rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        rng.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    }

                    //sheet.Name = "Rep_NotaEntrega";

                    var rowIndex = 2;
                    foreach (var item in oMatriz)
                    {
                        var col = 1;

                        if (item.Nivel == 1)
                        {
                            var cell = sheet.Cells[rowIndex, 1];
                            cell.Style.Font.Bold = true;
                        }

                        sheet.Cells[rowIndex, col++].Value = item.Nombre;
                        sheet.Cells[rowIndex, col++].Value = item.UnidadDeMedida;
                        sheet.Cells[rowIndex, col++].Value = item.Ene;
                        sheet.Cells[rowIndex, col++].Value = item.Feb;
                        sheet.Cells[rowIndex, col++].Value = item.Mar;
                        sheet.Cells[rowIndex, col++].Value = item.Abr;
                        sheet.Cells[rowIndex, col++].Value = item.May;
                        sheet.Cells[rowIndex, col++].Value = item.Jun;
                        sheet.Cells[rowIndex, col++].Value = item.nTotal_I_S;
                        sheet.Cells[rowIndex, col++].Value = item.nAvance1;
                        sheet.Cells[rowIndex, col++].Value = item.cMotivoRestraso1;
                        sheet.Cells[rowIndex, col++].Value = item.cLogro1;
                        rowIndex++;
                    }

                    using (ExcelRange rng = sheet.Cells["A1:L" + (rowIndex - 1).ToString()])
                    {
                       rng.Style.Border.Top.Style = rng.Style.Border.Left.Style = rng.Style.Border.Bottom.Style = rng.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    }

                    using (ExcelRange rng = sheet.Cells["I1:I" + (rowIndex - 1).ToString()])
                    {
                        rng.Style.Font.Bold = true;
                    }

                    using (ExcelRange col = sheet.Cells[2, 10, 1 + oMatriz.Count, 10])
                    {
                        col.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        col.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(140, 173, 232));
                    }

                    using (ExcelRange col = sheet.Cells[2, 2, 1 + oMatriz.Count, 10])
                    {
                        //col.Style.Numberformat.Format = "#,##0.00";
                        col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        col.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    }

                    sheet.Column(3).Width = 4;
                    sheet.Column(4).Width = 4;
                    sheet.Column(5).Width = 5;
                    sheet.Column(6).Width = 4;
                    sheet.Column(7).Width = 5;
                    sheet.Column(8).Width = 4;
                    sheet.Column(9).Width = 14;
                    sheet.Column(10).Width = 12.57;
                    sheet.Column(11).Width = 30;
                    sheet.Column(12).Width = 30;



                }
                else if (nPeriodo == 2)
                {
                    sheet.Cells["C1"].Value = "Jul";
                    sheet.Cells["D1"].Value = "Ago";
                    sheet.Cells["E1"].Value = "Sep";
                    sheet.Cells["F1"].Value = "Avance Programado";
                    sheet.Cells["G1"].Value = "Avance III Trim.";
                    sheet.Cells["H1"].Value = "Motivo del Retraso";
                    sheet.Cells["I1"].Value = "Logros más importantes";

                    using (ExcelRange rng = sheet.Cells["A1:I1"])
                    {
                        rng.Style.Font.Bold = true;
                        rng.Style.Fill.PatternType = ExcelFillStyle.Solid;                      //Set Pattern for the background to Solid
                        rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(242, 242, 242));  //Set color to dark blue
                        rng.Style.Font.Color.SetColor(Color.Black);
                        rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        rng.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    }


                    //sheet.Name = "Rep_NotaEntrega";

                    var rowIndex = 2;
                    foreach (var item in oMatriz)
                    {
                        var col = 1;

                        if (item.Nivel == 1)
                        {
                            var cell = sheet.Cells[rowIndex, 1];
                            cell.Style.Font.Bold = true;
                        }

                        sheet.Cells[rowIndex, col++].Value = item.Nombre;
                        sheet.Cells[rowIndex, col++].Value = item.UnidadDeMedida;
                        sheet.Cells[rowIndex, col++].Value = item.Jul;
                        sheet.Cells[rowIndex, col++].Value = item.Ago;
                        sheet.Cells[rowIndex, col++].Value = item.Sep;
                        sheet.Cells[rowIndex, col++].Value = item.nTotal_III_T;
                        sheet.Cells[rowIndex, col++].Value = item.nAvance2;
                        sheet.Cells[rowIndex, col++].Value = item.cMotivoRestraso2;
                        sheet.Cells[rowIndex, col++].Value = item.cLogro2;
                        rowIndex++;
                    }


                    using (ExcelRange rng = sheet.Cells["A1:I" + (rowIndex - 1).ToString()])
                    {
                        rng.Style.Border.Top.Style = rng.Style.Border.Left.Style = rng.Style.Border.Bottom.Style = rng.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    }

                    using (ExcelRange rng = sheet.Cells["F1:F" + (rowIndex - 1).ToString()])
                    {
                        rng.Style.Font.Bold = true;
                    }

                    using (ExcelRange col = sheet.Cells[2, 7, 1 + oMatriz.Count, 7])
                    {
                        col.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        col.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(140, 173, 232));
                    }

                    using (ExcelRange col = sheet.Cells[2, 2, 1 + oMatriz.Count, 7])
                    {
                        col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        col.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    }

                    sheet.Column(3).Width = 4;
                    sheet.Column(4).Width = 5;
                    sheet.Column(5).Width = 4;
                    sheet.Column(6).Width = 14;
                    sheet.Column(7).Width = 12.57;
                    sheet.Column(8).Width = 30;
                    sheet.Column(9).Width = 30;

                }
                else if (nPeriodo == 3)
                {
                    sheet.Cells["C1"].Value = "Oct";
                    sheet.Cells["D1"].Value = "Nov";
                    sheet.Cells["E1"].Value = "Dic";
                    sheet.Cells["F1"].Value = "Avance Programado";
                    sheet.Cells["G1"].Value = "Avance IV Trim.";
                    sheet.Cells["H1"].Value = "Motivo del Retraso";
                    sheet.Cells["I1"].Value = "Logros más importantes";

                    using (ExcelRange rng = sheet.Cells["A1:I1"])
                    {
                        rng.Style.Font.Bold = true;
                        rng.Style.Fill.PatternType = ExcelFillStyle.Solid;                      //Set Pattern for the background to Solid
                        rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(242, 242, 242));  //Set color to dark blue
                        rng.Style.Font.Color.SetColor(Color.Black);
                        rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        rng.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    }

                    //sheet.Name = "Rep_NotaEntrega";

                    var rowIndex = 2;
                    foreach (var item in oMatriz)
                    {
                        var col = 1;

                        if (item.Nivel == 1)
                        {
                            var cell = sheet.Cells[rowIndex, 1];
                            cell.Style.Font.Bold = true;
                        }

                        sheet.Cells[rowIndex, col++].Value = item.Nombre;
                        sheet.Cells[rowIndex, col++].Value = item.UnidadDeMedida;
                        sheet.Cells[rowIndex, col++].Value = item.Oct;
                        sheet.Cells[rowIndex, col++].Value = item.Nov;
                        sheet.Cells[rowIndex, col++].Value = item.Dic;
                        sheet.Cells[rowIndex, col++].Value = item.nTotal_IV_T;
                        sheet.Cells[rowIndex, col++].Value = item.nAvance3;
                        sheet.Cells[rowIndex, col++].Value = item.cMotivoRestraso3;
                        sheet.Cells[rowIndex, col++].Value = item.cLogro3;
                        rowIndex++;
                    }

                    using (ExcelRange rng = sheet.Cells["A1:I" + (rowIndex - 1).ToString()])
                    {
                        rng.Style.Border.Top.Style = rng.Style.Border.Left.Style = rng.Style.Border.Bottom.Style = rng.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    }

                    using (ExcelRange rng = sheet.Cells["F1:F" + (rowIndex - 1).ToString()])
                    {
                        rng.Style.Font.Bold = true;
                    }

                    using (ExcelRange col = sheet.Cells[2, 7, 1 + oMatriz.Count, 7])
                    {
                        col.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        col.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(140, 173, 232));
                    }

                    using (ExcelRange col = sheet.Cells[2, 2, 1 + oMatriz.Count, 7])
                    {
                        col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        col.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    }

                    sheet.Column(3).Width = 4;
                    sheet.Column(4).Width = 5;
                    sheet.Column(5).Width = 4;
                    sheet.Column(6).Width = 14;
                    sheet.Column(7).Width = 12.57;
                    sheet.Column(8).Width = 30;
                    sheet.Column(9).Width = 30;

                }
                
                sheet.Column(1).Width = 34;
                sheet.Column(2).Width = 18;


                using (ExcelRange col = sheet.Cells[1, 1, 1 + oMatriz.Count, 13])
                {
                    col.Style.WrapText = true;
                    col.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                }


                //sheet.Column(9).Style.Numberformat.Format = "#,##0.00";
                // You could just save on ExcelPackage here but we need it in 
                // memory to stream it back to the browser
                Response.ClearContent();
                Response.BinaryWrite(excelPackage.GetAsByteArray());
                Response.AddHeader("content-disposition",
                          "attachment;filename=Meta_"+ cPlanText + "_" + cPeriodo + "_" + cHoy + ".xlsx");
                Response.ContentType = "application/excel";
                Response.Flush();
                Response.End();
            }
        }


        [RequiresAuthenticationAttribute]
        public JsonResult OptenerIndicadores(Int32 InstanciaId, Int32 nPeriodo)
        {
            Indicadores oEficaFi = new Indicadores();
            Usuario oUsuario = new Usuario();
            oUsuario = (Usuario)Session["Datos"];

            oEficaFi = handlerAvance.OptenerIndicadores(InstanciaId,oUsuario.UserId, nPeriodo);
            return Json(JsonConvert.SerializeObject(oEficaFi));
        }

        [RequiresAuthenticationAttribute]
        public JsonResult RegistrarAvancePOI
            (
                int InstanciaId,
                int? nAvance1, int? nMotivoRestraso1, string cLogro1,
                int nAvance2, int? nMotivoRestraso2, string cLogro2,
                int nAvance3, int? nMotivoRestraso3, string cLogro3
            )
        {
            itemAvance oItemAva = new itemAvance();
            oItemAva = (itemAvance)Session["itemAvance"];
            RespuestaViewModel response=new RespuestaViewModel();
            RepMonitoreoPOI item = new RepMonitoreoPOI();
            item.InstanciaId = InstanciaId;
            item.PlanOperativoId = oItemAva.PlanOperativoId;
            item.nAvance1 = nAvance1;
            item.nMotivoRestraso1 = nMotivoRestraso1;
            item.cLogro1 = cLogro1;
            item.nAvance2 = nAvance2;
            item.nMotivoRestraso2 = nMotivoRestraso2;
            item.cLogro2 = cLogro2;
            item.nAvance3 = nAvance3;
            item.nMotivoRestraso3 = nMotivoRestraso3;
            item.cLogro3 = cLogro3;
            response.Respuesta=handlerAvance.registrarAvance(item, oItemAva.nPeriodo);
            if (response.Respuesta!="")
            {
                response.Estado = 1;
            }
            else
            {
                response.Estado = 0;
                response.Respuesta = "Ocurrió un fallo";
            }

            return Json(JsonConvert.SerializeObject(response));
        }
        
    }
}
