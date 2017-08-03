using System.Web;
using System.Web.Optimization;

namespace Bounds
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/utils.js",
                        "~/Scripts/jquery.unobtrusive-ajax.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/bootstrap-multiselect.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/bootstrap-datetimepicker.min.css",
                      "~/Content/bootstrap-multiselect.css"
                      ));

            bundles.Add(new StyleBundle("~/Content/zTree").Include(
                        "~/Content/zTreeStyle.css"));

            bundles.Add(new StyleBundle("~/Content/contextmenu").Include(
                        "~/Content/bootstrap-combined.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/zTree").Include(
                        "~/Scripts/jquery.ztree.core.js",
                        "~/Scripts/jquery.ztree.excheck.js"));

            bundles.Add(new ScriptBundle("~/bundles/zTree-user").Include(
                        "~/Scripts/jquery.ztree.core.js",
                        "~/Scripts/jquery.ztree.excheck.js",
                        "~/Scripts/ztree-check-user.js"));

            bundles.Add(new ScriptBundle("~/bundles/zTree-ext").Include(
                        "~/Scripts/ztree-ext.js"));

            bundles.Add(new ScriptBundle("~/bundles/zTreeCheck").Include(
                        "~/Scripts/ztree-check-ext.js"));

            bundles.Add(new ScriptBundle("~/bundles/contextmenu").Include(
                        "~/Scripts/bootstrap-contextmenu.js",
                        "~/Scripts/prettify.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/datetime").Include(
                "~/Scripts/Moment.js",
                "~/Scripts/bootstrap-datetimepicker.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/editable").Include(
                "~/Scripts/bootstrap-editable.js"
                ));
            bundles.Add(new StyleBundle("~/Content/editable").Include(
                        "~/Content/bootstrap-editable.css"));

            bundles.Add(new ScriptBundle("~/bundles/login").Include(
                "~/Scripts/zzsc.js"
                ));
            bundles.Add(new StyleBundle("~/Content/login").Include(
                        "~/Content/zzsc.css"));
        }
    }
}
