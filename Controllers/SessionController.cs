using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mvc_crud.Controllers
{
    public class SessionController : Controller
    {
        // GET: Session
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Submit()
        {
            String user = Request.Form["user"];
            String pass = Request.Form["password"];
            if (Access.Login(user, pass))
            {
                Session["ID"] = "0";
                Session["email"] = user;
                Session["pass"] = pass;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Login", "Session");
            }
        }
        public ActionResult MyDashboard()
        {
            return View();
        }
        public ActionResult Logout()
        {
            Session.Remove("ID");
            return View("Login");
        }
    }
}