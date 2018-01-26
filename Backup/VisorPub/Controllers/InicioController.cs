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

namespace VisorPub.Controllers
{
    public class InicioController : Controller
    {

        public class itemAvance
        {
            public int PlanOperativoId;
            public int nPeriodo;
        }
        //
        // GET: /Inicio/
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
        [RequiresAuthenticationAttribute]
        public JsonResult ListarMetasDeResponsableMeta(Int32 PlanOperativoId, Int32 ResponsableMeta)
        {
            List<Combo> lsMetas = handlerAvance.ListarMetasDeResponsableMeta(PlanOperativoId, ResponsableMeta);
            return Json(JsonConvert.SerializeObject(lsMetas));
        }

        

        [RequiresAuthenticationAttribute]
        public JsonResult ListarMatrizPogramacion(Int32 InstanciaId, Int32 PlanOperativoId, Int32 nPeriodo)
        {
            itemAvance itemAvan = new itemAvance();
            itemAvan.PlanOperativoId = PlanOperativoId;
            itemAvan.nPeriodo = nPeriodo;
            Session["itemAvance"] = itemAvan;
            List<MatrizProgramacionFisica> lsMatriz = handlerAvance.ListaMatrizDeProgramacionFisica(InstanciaId);
            lsMatriz = handlerAvance.ordenarMatrizPorPadre(lsMatriz);
            return Json(JsonConvert.SerializeObject(lsMatriz));
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
