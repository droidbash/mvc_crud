using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using WebMatrix.Data;

namespace mvc_crud.Controllers
{
    public class EditController : Controller
    {
        // GET: Edit
        public ActionResult Table(String @table)
        {
            /*
            String respS = "";
            foreach(var row in resp)
            {
                respS += row.Id;
            }
            Debug.WriteLine(respS);
            */
            /*
            switch (table)
            {
                case "Cars": return Cars();
                default: return View();
            }
            Debug.WriteLine("ActionResult:" + @table);
            return RedirectToAction(controllerName:"Edit",actionName:"Cars");
            */
            return View();
        }
        public ActionResult Cars()
        {
            try
            {
                var dictionary = new Dictionary<string, object>();
                Database db = Database.Open("develop");
                dictionary.Add("data", db.Query("select * from GCAV.dbo.Cars"));
                //return Json(new { data = "[1,2]"});
                Debug.WriteLine(Json(new { data = "[1,2]"}).ToString());
                return null;
                //return System.Web.Helpers.Json.Encode(dictionary);
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}