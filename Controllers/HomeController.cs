using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace mvc_crud.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Descripción de la aplicación web.";
            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Página de contacto.";
            return View();
        }
    }
}