using System.Web.Mvc;

namespace Website
{
    public static class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ErrorHandler.AiHandleErrorAttribute());
            filters.Add(new ApplicationInsights.HandleErrorAttribute());
        }
    }
}
