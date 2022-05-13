using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvc_crud.Models;

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
            UserModel userModel = Access.Login(user, pass);
            if (userModel.Id == 0)
            {
                return RedirectToAction("Login", "Session");
            }
            else
            {
                Session["id"] = userModel.Id;
                Session["user"] = userModel;
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult MyDashboard()
        {
            return View();
        }
        public ActionResult Logout()
        {
            Session.Remove("user");
            return View("Login");
        }
        public ActionResult Unauthored()
        {
            return View();
        }
    }
}