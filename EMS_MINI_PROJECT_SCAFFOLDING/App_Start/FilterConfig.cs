using System.Web;
using System.Web.Mvc;

namespace EMS_MINI_PROJECT_SCAFFOLDING
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
