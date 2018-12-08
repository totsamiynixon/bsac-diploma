using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebUI.Controllers;
using WebUI.Mapping;

namespace WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapperConfiguration.Configure();
            log4net.Config.XmlConfigurator.Configure();
        }

        protected void Application_Error()
        {
            if (HttpContext.Current.Request.RequestContext.HttpContext.Request.IsAjaxRequest())
            {
                return;
            }
            Response.TrySkipIisCustomErrors = true;

            var exception = Server.GetLastError();

            Response.Clear();
            Server.ClearError();
            if(exception is HttpException)
            {
                var httpex = exception as HttpException;
                Response.StatusCode = httpex.GetHttpCode();
                return;
            }
            Response.StatusCode = 500;
            Response.Write("Error!");
        }
    }
}
