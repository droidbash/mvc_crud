using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using WebMatrix.Data;
using Newtonsoft.Json;

namespace mvc_crud.Controllers
{
    public class EditController : Controller
    {
        // GET: Edit
        public ActionResult Table(String @table)
        {
            return View();
        }
        private String SerializedTable(String table)
        {
            return JsonConvert.SerializeObject(
                table == null
                    ? new Dictionary<string, object>() { { "data", "" } }
                    : new Dictionary<string, object>() { { "data", QueryTable("develop", "*", table) } }
                );
        }
        private IEnumerable<dynamic> QueryTable(String connectionName, String columns, String table)
        {
            return Database.Open(connectionName).Query(String.Format("select {0} from GCAV.dbo.{1}", columns, table));
        }
        private IEnumerable<dynamic> QueryRow(String connectionName, String columns, String table, String id)
        {
            return Database.Open(connectionName).Query(String.Format("select {0} from GCAV.dbo.{1} where Id = {2}", columns, table, id));
        }
        [HttpPost]
        public String DataTable(String table)
        {
            return SerializedTable(table);
        }
        [HttpPost]
        public String GetRowData(String table, String id)
        {
            return JsonConvert.SerializeObject(QueryRow("develop", "*", table, id));
        }
        private bool EditRow(HttpRequestBase Request)
        {
            String table = "";
            String Id = "";
            String sets = "";
            try
            {
                int x = 0;
                foreach (var key in Request.Form.AllKeys)
                {
                    x++;
                    if (key == "table")
                        table = Request.Form[key];
                    else if (key == "Id")
                        Id = Request.Form[key];
                    else if (key != "function")
                        sets += String.Format("{0}='{1}',", key, Request.Form[key]);
                }
                sets = sets.Substring(0, sets.Length - 1);
                Database.Open("develop").Query(String.Format("update GCAV.dbo.{0} set {1} where Id = {2}", table, sets, Id));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool AddRow(HttpRequestBase Request)
        {
            String table = "";
            String keys = "";
            String values = "";
            try
            {
                int x = 0;
                foreach (var key in Request.Form.AllKeys)
                {
                    x++;
                    if (key == "table")
                    {
                        table = Request.Form[key];
                    }
                    else if (key != "function" && key != "Id")
                    {
                        keys += String.Format("{0},", key);
                        values += String.Format("'{0}',", Request.Form[key]);
                    }
                }
                keys = keys.Substring(0, keys.Length - 1);
                values = values.Substring(0, values.Length - 1);
                Database.Open("develop").Query(String.Format("insert into GCAV.dbo.{0}({1}) values ({2})", table, keys, values));
                return true;
            } catch (Exception)
            {
                return false;
            }
        }
        private bool DeleteRow(HttpRequestBase Request)
        {
            String table = Request.Form["table"];
            String Id = Request.Form["Id"];
            try
            {
                Database.Open("develop").Query(String.Format("delete from GCAV.dbo.{0} where Id = '{1}'", table, Id));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        [HttpPost]
        public ActionResult Modal()
        {
            String function = Request.Form["function"];
            switch(function)
            {
                case "add": return Json(new { success = AddRow(Request) });
                case "edit": return Json(new { success = EditRow(Request)});
                case "delete": return Json(new { success = DeleteRow(Request) });
                default: return Json(new { success = false });
            }
        }
    }
}