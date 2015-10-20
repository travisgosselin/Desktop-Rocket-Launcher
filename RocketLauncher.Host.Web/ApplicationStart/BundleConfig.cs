using System.Web;
using System.Web.Optimization;

namespace RocketLauncher.Host.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // scripts
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Assets/lib/jquery/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Assets/lib/bootstrap/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                     "~/Assets/lib/angular/angular.js",
                     "~/Assets/lib/angular/angular-resource.js",
                     "~/Assets/lib/angular/angular-route.js"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                    "~/Assets/app/*.module.js",
                    "~/Assets/app/*.js"));

            // styles
            bundles.Add(new StyleBundle("~/bundles/css").Include(
                      "~/Assets/lib/bootstrap/bootstrap.css",
                      "~/Assets/app/*.css"));
        }
    }
}
