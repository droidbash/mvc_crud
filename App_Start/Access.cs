using System.Web;
using System.Web.Optimization;
using System.Diagnostics;

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
        public static bool Login(string user, string pass)
        {
            return user == "drockandroid@gmail.com" && pass == "asd";
        }
    }
}
