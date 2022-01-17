using System.Web.Optimization;

namespace Heuristics.TechEval.Web {
	public class BundleConfig {
		// For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles) {
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
						"~/Scripts/jquery/jquery-{version}.js"));

			bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
						"~/Scripts/jquery/jquery.validate*"));

			bundles.Add(new ScriptBundle("~/bundles/jquery.form").Include(
						"~/Scripts/jquery/jquery.form.min.js"));

			// Use the development version of Modernizr to develop with and learn from. Then, when you're
			// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
						"~/Scripts/modernizr/modernizr-*"));

			bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
					  "~/Scripts/bootstrap/bootstrap.js",
					  "~/Scripts/bootstrap/respond.js"));

			bundles.Add(new StyleBundle("~/Content/css").Include(
					  "~/Content/bootstrap.css",
					  "~/Content/site.css"));


			bundles.Add(new ScriptBundle("~/bundles/HSScripts").Include(
					  "~/Scripts/HS/members.js"));
		}
	}
}
