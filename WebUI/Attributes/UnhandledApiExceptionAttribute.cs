using System;
using System.Web;
using System.Web.Http.Filters;
using Contracts;
using log4net;
using Newtonsoft.Json;

namespace WebUI.Attributes
{
    public class UnhandledApiExceptionAttribue : ExceptionFilterAttribute
    {
        private static readonly ILog Log = LogManager.GetLogger("logger");

        public override void OnException(HttpActionExecutedContext context)
        {
            var filterContext = context.ActionContext.RequestContext;
            var controllerName = (string)filterContext.RouteData.Values["controller"];
            var actionName = (string)filterContext.RouteData.Values["action"];

            var queryString = context.Request.RequestUri.Query;

            var message =
                "API Error" + Environment.NewLine +
                $"There was an error in controller '{controllerName}Controller' " +
                $"and action '{actionName}'{Environment.NewLine} " +
                $"Params: {queryString}{Environment.NewLine}";

            if (!(context.Exception is ILogUnhandledException || context.Exception is HttpException))
            {
                Log.Error(message, context.Exception);
                if (context.Exception.InnerException != null)
                {
                    Log.Error(message, context.Exception.InnerException);
                }
            }

            base.OnException(context);
        }
    }
}