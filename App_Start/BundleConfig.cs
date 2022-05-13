using System.Web;
using System.Web.Optimization;

namespace mvc_crud
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //========================================================================================================================================================
            //                             ~/VirtualPath                    ~/FileLocations                                                                         //
            //                                   |                                  |                                                                               //
            //                                  _|_                                _|_                                                                              //
            //                                  \ /                                \ /                                                                              //
            //                                   "                                  "                                                                               //
                bundles.Add(new ScriptBundle("~/VirtualPath").Include(  "~/FileLocation/FileName1.Extension",                                                       //
                                                                        "~/FileLocation/FileName2.Extension",                                                       //
                                                                        "~/FileLocation/FileNameN.Extension"));                                                     //
            //Estos bundles se pueden llamar desde cualquier parte de la vista con "@Scripts.Render("~/VirtualPath")" que llamará a los archivos de ~/FileLocation. //
            //De la misma forma se pueden declarar estilos por medio de StyleBundle() y llamarlos con Styles.Render().                                              //
            //========================================================================================================================================================
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/bootstrap.css","~/Content/site.css"));
            bundles.Add(new StyleBundle("~/Content/signin").Include("~/Content/signin.css"));
            bundles.Add(new StyleBundle("~/Content/datatables").Include("~/Datatables/datatables.min.css"));
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-3.6.0.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include("~/Scripts/jquery.validate*")); //https://blog.trescomatres.com/2020/06/jquery-validation-validar-segun-una-regla-personalizada/
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/modernizr-*"));
            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/bootstrap.js"));
            bundles.Add(new Bundle("~/bundles/bootstrap").Include("~/Scripts/bootstrap.js"));
            bundles.Add(new Bundle("~/bundles/datatables").Include("~/Datatables/datatables.min.js"));
        }
    }
}
