using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using WebMatrix.Data;
using Newtonsoft.Json;
using mvc_crud.Models;

namespace mvc_crud.Controllers
{
    public class DriverController : Controller
    {
        // GET: Driver
        public ActionResult Enroll()
        {
            return View();
        }
        public ActionResult EnrolledTournaments()
        {
            return View();
        }
        public ActionResult Teams()
        {
            return View();
        }
        public ActionResult JoinTeam()
        {
            return View();
        }
        [HttpPost]
        public ActionResult EnrollTournament()
        {
            try
            {
                UserModel user = (UserModel)Session["user"];
                String enrollDriver = String.Format("INSERT INTO [GCAV].[dbo].[DriverTournament] ([Tournament],[User],[Car]) VALUES ('{0}','{1}','{2}')", Request.Form["tournament"], user.Id, Request.Form["car"]);
                Database.Open("develop").Query(enrollDriver);
                return Json(new { success = true });
            } catch (Exception)
            {
                return Json(new { success = false});
            }

        }
    }
}