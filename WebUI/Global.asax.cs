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

            IController newController = new ErrorController();
            var routeData = new RouteData();
            routeData.Values.Add("controller", "Error");
            if (exception is HttpException)
            {
                routeData.Values.Add("action", "Error404");
            }
            else if (exception is IBusinessError)
            {
                routeData.Values.Add("action", "CustomError");
                routeData.Values.Add("exception", exception);
            }
            else
            {
                routeData.Values.Add("action", "Error500");
            }

            var newRequestContext = new RequestContext(new HttpContextWrapper(HttpContext.Current), routeData);
            newController.Execute(newRequestContext);
        }
    }
}
