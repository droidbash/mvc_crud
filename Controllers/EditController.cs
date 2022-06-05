using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using WebMatrix.Data;
using Newtonsoft.Json;
using System.Threading.Tasks;

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
        [HttpPost]
        public ActionResult SaveRow()
        {
            return Json(new { data = ""});
        }
        /*
        [HttpPost]
        public async Task<ActionResult> EditTableRow()
        {
            var function = Request.Form["function"];
            Debug.WriteLine(function);
            string query;
            switch (function)
            {
                case "edit":
                    //query = QueryEditDriver(Request);
                    break;
                case "add":
                    //query = QueryAddDriver(Request);
                    break;
                default:
                    return View("NotImplemented");
            }
            try
            {
                //await QueryAsync(SettingsDevelop, query);
                return RedirectToAction("Index");
                //return View("Index", await GetDriversAsync());
            }
            catch (Exception e)
            {
                //return View("QueryError", new QueryError() { Msg = "Error al procesar la solicitud en la base de datos.", Error = e.Message });
            }
            return null;
        }

        */
    }
}