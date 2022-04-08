/*
 * =====================================================================================================================================
 * USINGS
 */
using System;
using mvc_crud.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Helpers;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Web;
/*
* =====================================================================================================================================
* NOT USED
*/
//using System.Linq;
//using System.Web;
namespace mvc_crud.Controllers
{
    public class CrudController : Controller
    {
        string driversQuery = "SELECT id, name, nationality, age, active FROM CRUDMVC.dbo.Driver order by name";
        ConnectionStringSettings SettingsDevelop = ConfigurationManager.ConnectionStrings["develop"];
        public ActionResult Index()
        {
            return View(GetDrivers());
        }
        [HttpPost]
        public JsonResult DriverSelect(int id)
        {
            foreach(DriverModel driver in GetDrivers())
            {
                if(driver.Id == id)
                    return Json(JsonConvert.SerializeObject(driver));
            }
            return null;
        }
        [HttpPost]
        public string QueryAddDriver(HttpRequestBase request)
        {
            var name = request.Form["name"];
            var nationality = request.Form["nationality"];
            var age = request.Form["age"];
            var active = request.Form["active"] == "on";
            return string.Format("insert into CRUDMVC.dbo.driver (name,nationality,age,active) values ('{0}','{1}','{2}','{3}')", name, nationality, age, active); ;
            /*
            return "";
            */
        }
        [HttpPost]
        public string QueryEditDriver(HttpRequestBase request)
        {
            var id = request.Form["id"];
            var name = request.Form["name"];
            var nationality = request.Form["nationality"];
            var age = request.Form["age"];
            var active = request.Form["active"] == "on";
            return string.Format("update CRUDMVC.dbo.driver set name = '{0}', nationality = '{1}', age = '{2}', active = '{3}' where id = '{4}'", name, nationality, age, active, id);
        }
        [HttpPost]
        public string QueryDeleteDriver(int id)
        {
            return string.Format("delete from CRUDMVC.dbo.driver where id = '{0}'", id);
        }
        public async Task<ActionResult> DriverModal()
        {
            var function = Request.Form["function"];
            Debug.WriteLine(function);
            string query;
            switch (function)
            {
                case "edit":
                    query = QueryEditDriver(Request);
                    break;
                case "add":
                    query = QueryAddDriver(Request);
                    break;
                default:
                    return View("NotImplemented");
            }
            try
            {
                await QueryAsync(SettingsDevelop, query);
                return RedirectToAction("Index");
                //return View("Index", await GetDriversAsync());
            }
            catch (Exception e)
            {
                return View("QueryError", new QueryError() { Msg = "Error al procesar la solicitud en la base de datos.", Error = e.Message });
            }
        }
        public async Task<ActionResult> DeleteDriver(int id)
        {
            Debug.WriteLine(id);
            await QueryAsync(SettingsDevelop, QueryDeleteDriver(id));
            return View("Index", await GetDriversAsync());
        }
        public async Task QueryAsync(ConnectionStringSettings settings, string query)
        {
            using (SqlConnection connection = new SqlConnection(settings.ConnectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = query;
                await command.ExecuteNonQueryAsync();
            }
        }
        public List<DriverModel> GetDrivers()
        {
            List<DriverModel> drivers = new List<DriverModel>() { };
            string connectionString = ConfigurationManager.ConnectionStrings["develop"].ConnectionString;
            
            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(driversQuery, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        drivers.Add(new DriverModel
                        {
                            Id = (int)reader["id"],
                            Name = (string)reader["name"],
                            Nationality = (string)reader["nationality"],
                            Age = (int)reader["age"],
                            Active = (bool)reader["active"],
                        });
                    }
                }
            }
            return drivers;
        }
        public async Task<List<DriverModel>> GetDriversAsync()
        {
            List<DriverModel> drivers = new List<DriverModel>() { };
            using (SqlConnection connection = new SqlConnection(SettingsDevelop.ConnectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = driversQuery;
                //await command.ExecuteNonQueryAsync();
                using (var reader = await command.ExecuteReaderAsync()) {
                    while (reader.Read())
                    {
                        drivers.Add(new DriverModel
                        {
                            Id = (int)reader["id"],
                            Name = (string)reader["name"],
                            Nationality = (string)reader["nationality"],
                            Age = (int)reader["age"],
                            Active = (bool)reader["active"],
                        });
                    }
                }
            }
            return drivers;
        }
    }
}