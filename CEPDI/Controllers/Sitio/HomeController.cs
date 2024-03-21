using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CEPDI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //Session["usuario"]="algo";
            if (Session.Keys.Count > 0 && Session["IdUsuario"] !=null && long.Parse(Session["IdUsuario"].ToString()) > 0) {

                return RedirectToAction("Index", "Usuarios", null);
            
            }
            return View();
        }

    }
}