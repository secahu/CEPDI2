using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CEPDI.Controllers.Sitio
{
    public class UsuariosController : Controller
    {
        // GET: Usuarios
        public ActionResult Index()
        {
            if ((Session.Keys == null || Session.Keys.Count == 0 )&& (Session["IdUsuario"] == null || long.Parse(Session["IdUsuario"].ToString())<=0)) {
                
                Session.Clear();
                return RedirectToAction("Index", "Home", null);

            }
            return View();
        }
        
        // GET: Usuarios
        public ActionResult Logout()
        {
            
            Session.Clear();
            return RedirectToAction("Index", "Home", null);
        }

    }
}