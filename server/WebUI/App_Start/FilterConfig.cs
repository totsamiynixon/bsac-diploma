using System.Web;
using System.Web.Mvc;
using WebUI.Attributes;
using WebUI.Filters;

namespace WebUI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new CustomHandleErrorAttribute());
         
        }
    }
}
