using System.Web;
using System.Web.Optimization;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using mvc_crud.Models;
using System;

namespace mvc_crud
{
    public class Access
    {
        static Dictionary<int, List<string>> levelAccess = new Dictionary<int, List<string>>() {
            { 0, new List<string>() {"/Crud" } }
        };
        private static int GetPathLevel(string path)
        {
            foreach (var row in levelAccess)
            {
                if (row.Value.Contains(path))
                    return row.Key;
            }
            return int.MaxValue;
        }
        public static void Verify(HttpSessionStateBase session, HttpRequestBase req, HttpResponseBase resp)
        {
            try
            {
                UserModel user = (UserModel) session["user"];
                int pathLevel = GetPathLevel(req.Url.AbsolutePath);
                if(user == null)
                {
                    resp.Redirect("~/Session/Login");
                }
                else
                {
                    if(user.Level > pathLevel)
                        resp.Redirect("~/Session/Unauthored");
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine("HttpSessionStateBase: null. Stacktrace:\n" + ex.StackTrace);
            }
        }
        public static UserModel Login(string email, string pass)
        {
            ConnectionStringSettings SettingsDevelop = ConfigurationManager.ConnectionStrings["develop"];
            string commandText = string.Format("select top 1 * from GCAV.dbo.Users as u where UPPER(u.email) = UPPER('{0}') and BINARY_CHECKSUM(u.pass) = BINARY_CHECKSUM('{1}')", email,pass);
            IEnumerable<IDataRecord> ieData = SelectQueryToIEnumerable(SettingsDevelop, commandText);
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
        }
        public static IEnumerable<IDataRecord> SelectQueryToIEnumerable(ConnectionStringSettings settings, string commandText)
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
        }
    }
}
