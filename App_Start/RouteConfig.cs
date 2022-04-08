using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace mvc_crud
{
    public class RouteConfig
    {
        /*
         * Metodo llamado de Global.asax.cs
         */
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                /*  
                 *  Definicion
                 *  controller: String
                 *      Requiere una clase que herede de Controller.
                 *  Definicion
                 *  action: String
                 *      Requiere un ActionResult del Controller especificado en controller.
                 *  Definicion
                 *  id: String
                 *      <- ¿?
                 *      No parece importante. Investigar esta propiedad.
                */
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
