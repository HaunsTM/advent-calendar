using System.Web.Optimization;

namespace advent_calendar
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));
            

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                    "~/Scripts/angular.js",
                    "~/Scripts/angular-ui-router.js",
                    "~/Scripts/ngStorage.js",
                    "~/Scripts/angular-ui/ui-bootstrap-tpls.js"));/**/

            bundles.Add(new ScriptBundle("~/bundles/angular/components").IncludeDirectory(
                    directoryVirtualPath: "~/angularJSApp/components",
                    searchPattern: "*.js", searchSubdirectories: true));

            bundles.Add(new ScriptBundle("~/bundles/angular/shared").IncludeDirectory(
                    directoryVirtualPath: "~/angularJSApp/shared",
                    searchPattern: "*.js", searchSubdirectories: true));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/bundles/styles").IncludeDirectory(
                      directoryVirtualPath: "~/angularJSApp/styles",
                      searchPattern: "*.css", searchSubdirectories: true));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = false;
        }
    }
}
