using System.Web;
using System.Web.Optimization;

namespace WoW.Web
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));

            bundles.Add(new StyleImagePathBundle("~/WOW/MasterCSS").Include(
                    "~/Resources/global/vendors/font-awesome/css/font-awesome.min.css",
                    "~/Resources/global/vendors/simple-line-icons/simple-line-icons.css",
                    "~/Resources/global/vendors/bootstrap/css/bootstrap.min.css",
                    "~/Resources/global/vendors/animate.css/animate.css",
                    "~/Resources/assets/vendors/bootstrap-switch/css/bootstrap-switch.css",
                    "~/Resources/assets/vendors/google-code-prettify/prettify.css",
                    "~/Resources/assets/vendors/jquery-jvectormap/jquery-jvectormap-2.0.1.css",
                    "~/Resources/global/css/core.css",
                    "~/Resources/assets/css/system.css",
                    "~/Resources/assets/css/system-responsive.css"
                    ));

            bundles.Add(new ScriptBundle("~/WOW/MasterJS").Include(
                    "~/Resources/global/js/jquery-1.10.2.min.js",
                    "~/Resources/global/js/jquery-migrate-1.2.1.min.js",
                    "~/Resources/global/js/jquery-ui.js",
                    "~/Resources/global/vendors/bootstrap/js/bootstrap.min.js",
                    "~/Resources/global/vendors/bootstrap-hover-dropdown/bootstrap-hover-dropdown.js",
                    "~/Resources/global/js/html5shiv.js",
                    "~/Resources/global/js/respond.min.js",
                    "~/Resources/global/vendors/metisMenu/jquery.metisMenu.js",
                    "~/Resources/global/vendors/slimScroll/jquery.slimscroll.js",
                    "~/Resources/global/vendors/iCheck/icheck.min.js",
                    "~/Resources/global/vendors/iCheck/custom.min.js",
                    "~/Resources/assets/vendors/bootstrap-switch/js/bootstrap-switch.min.js",
                    "~/Resources/assets/vendors/google-code-prettify/prettify.js",
                    "~/Resources/assets/vendors/jquery-cookie/jquery.cookie.js",
                    "~/Resources/assets/vendors/jquery.pulsate.js",
                    "~/Resources/global/js/core.js",
                    "~/Resources/assets/js/system-layout.js",
                    "~/Resources/assets/js/jquery-responsive.js"));

            bundles.Add(new StyleImagePathBundle("~/WOW/PostingCSS").Include(
                    "~/Resources/assets/vendors/bootstrap-timepicker/css/bootstrap-timepicker.min.css",
                    "~/Resources/assets/css/jquery-ui-1.10.3.custom.css",
                    "~/Resources/assets/css/vendors.css"
                    ));

            bundles.Add(new ScriptBundle("~/WOW/PostingJS").Include(
                    "~/Resources/assets/vendors/jquery-validation/dist/jquery.validate.js",
                    "~/Resources/assets/js/form-validation.js",
                    "~/Resources/assets/vendors/bootstrap-timepicker/js/bootstrap-timepicker.js",
                    "~/Resources/assets/js/form-picker.js",
                    "~/Resources/assets/js/ui-sliders.js"
                    ));
        }
    }
}