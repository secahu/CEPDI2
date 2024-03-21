using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CEPDI.Controllers.Sitio
{
    public class MedicinasController : Controller
    {
        // GET: Medicinas
        public ActionResult Index()
        {
            if ((Session.Keys == null || Session.Keys.Count == 0) && (Session["IdUsuario"] == null || long.Parse(Session["IdUsuario"].ToString()) <= 0))
            {

                Session.Clear();
                return RedirectToAction("Index", "Home", null);

            }
            return View();
        }
    }
}