using mvc_crud.Models;
    /*Collections*/
using System.Collections.Generic;
    /*Database*/
using System.Data.SqlClient;
using System.Configuration;
    /*MVC*/
using System.Web.Mvc;

    /*NOT USED*/
//using System;
//using System.Linq;
//using System.Web;

    /*DEVELOP*/
using System.Diagnostics;

namespace mvc_crud.Controllers
{
    public class CrudController : Controller
    {
        // GET: Crud
        public ActionResult Index()
        {
            List<DriverModel> drivers = new List<DriverModel>() { };
            string connectionString = ConfigurationManager.ConnectionStrings["develop"].ConnectionString;
            string queryString = "SELECT name, nationality, age, active FROM CRUDMVC.dbo.Driver";
            using (var connection = new SqlConnection(connectionString)) {
                var command = new SqlCommand(queryString, connection);
                connection.Open();
                using (var reader = command.ExecuteReader()) {
                    while(reader.Read()) {
                        drivers.Add(new DriverModel {
                            Name = (string) reader["name"],
                            Nationality = (string) reader["nationality"],
                            Age = (int) reader["age"],
                            Active = ((int) reader["active"]) == 1,
                        });
                        //Debug.WriteLine(reader["age"]);
                    }
                }
            }
            return View(drivers);
        }

        public ActionResult TestView() {
            List<DriverModel> m = new List<DriverModel>() {
                new DriverModel { Name = "NAME", Nationality = "NATIONALITY", Age = 20, Active = true }
            };
            return View(m);
        }
    }
}