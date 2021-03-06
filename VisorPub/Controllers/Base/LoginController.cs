﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using VisorPub.Controllers.Base;
using VisorPub.Models;
using ESql;


namespace VisorPub.Controllers.Base
{
    public class LoginController : BaseController   
    {
        public IFormsAuthenticationService FormsService { get; set; }
        public IMembershipService MembershipService { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            if (FormsService == null) { FormsService = new FormsAuthenticationService(); }
            if (MembershipService == null) { MembershipService = new AccountMembershipService(); }

            base.Initialize(requestContext);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Logear()
        {
            string UserName = Request["txtusuario"];
            string Password = Request["txtpassword"];
            //validamos usuario
            Password = Helpers.Funciones.GenerarMD5(Password);
            bool validar = Membership.Provider.ValidateUser(UserName, Password);
            if (validar)
            {
                //registramos usuario
                FormsService.SignIn(UserName, true);
                Usuario oUsuario = new Usuario();
                oUsuario = (Usuario)Session["Datos"];
            }
            // Si llegamos a este punto, es que se ha producido un error y volvemos a mostrar el formulario
            return RedirectToAction("Index", "Inicio");
        }

        public ActionResult CerrarSession()
        {
            FormsService.SignOut();
            Roles.DeleteCookie();
            Session.RemoveAll();
            return RedirectToAction("Login", "Login");
        }

    }
}
