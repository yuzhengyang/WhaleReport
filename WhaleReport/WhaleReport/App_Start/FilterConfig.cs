using System.Web;
using System.Web.Mvc;
using WhaleReport.Modules.AuthorizeModule;

namespace WhaleReport
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new Authorize());
        }
    }
}
