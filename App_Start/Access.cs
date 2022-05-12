using System.Web;
using System.Web.Optimization;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using mvc_crud.Models;

namespace mvc_crud
{
    public class Access
    {
        public static void Verify(HttpSessionStateBase session, HttpResponseBase response)
        {
            if(session != null)
            {
                if(session["ID"] == null)
                {
                    response.Redirect("~/Session/Login");
                }
                else
                {
                    Debug.Write("ID: " + session["ID"]);
                }
            }
            else
            {
                Debug.WriteLine("HttpSessionStateBase: null");
            }
        }
        public static UserModel Login(string email, string pass)
        {
            ConnectionStringSettings SettingsDevelop = ConfigurationManager.ConnectionStrings["develop"];
            string commandText = string.Format("select top 1 * from GCAV.dbo.Users as u where UPPER(u.email) = UPPER('{0}') and BINARY_CHECKSUM(u.pass) = BINARY_CHECKSUM('{1}')", email,pass);
            IEnumerable<IDataRecord> ieData = ExecQuery2(SettingsDevelop, commandText);
            UserModel user = new UserModel(0);
            foreach(IDataRecord record in ieData)
            {
                int idL = (int) record["Id"];
                string name = (string) record["Name"];
                string emailL = (string) record["Email"];
                string passL = (string) record["Pass"];
                int level = (int) record["Level"];
                if(email == emailL && pass == passL)
                {
                    user.Id = idL;
                    user.Email = email;
                    user.Name = name;
                    user.Level = level;
                    break;
                }
            }
            return user;
            //return user == "drockandroid@gmail.com" && pass == "asd";
        }
        public static IEnumerable<IDataRecord> ExecQuery2(ConnectionStringSettings settings, string commandText)
        {
            using (SqlConnection connection = new SqlConnection(settings.ConnectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = commandText;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        yield return reader;
                    }
                }
            }
            /*
            using (var reader = await command.ExecuteReaderAsync())
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
            */
        }
    }
}
