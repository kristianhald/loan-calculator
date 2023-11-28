using System.Web.Optimization;
using System.Web.Optimization.React;

namespace Website
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
#if DEBUG
            BundleTable.EnableOptimizations = false;
#else
            BundleTable.EnableOptimizations = true;
#endif

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/cookies").Include(
                "~/Scripts/cookies/*.js"));

            bundles.Add(new BabelBundle("~/bundles/loanApp").Include(
                "~/Scripts/react/react.js",
                "~/Scripts/react/react-dom.js",
                "~/Scripts/loanApp/htmlTypes/currencyInput.jsx",
                "~/Scripts/loanApp/header.jsx",
                "~/Scripts/loanApp/ownPaymentInformation.jsx",
                "~/Scripts/loanApp/bankLoanInformation.jsx",
                "~/Scripts/loanApp/residenceInformation.jsx",
                "~/Scripts/loanApp/input.jsx",
                "~/Scripts/loanApp/companyResultsOverview.jsx",
                "~/Scripts/loanApp/companyResultsDetailed.jsx",
                "~/Scripts/loanApp/resultsOverview.jsx",
                "~/Scripts/loanApp/disclaimer.jsx",
                "~/Scripts/loanApp/application.jsx",
                "~/Scripts/loanApp/render.jsx"
            ));
        }
    }
}
