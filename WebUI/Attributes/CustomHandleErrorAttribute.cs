using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Contracts;
using log4net;
using Newtonsoft.Json;

namespace WebUI.Attributes
{
    public class CustomHandleErrorAttribute : HandleErrorAttribute
    {
        private static readonly ILog Log = LogManager.GetLogger("logger");

        public override void OnException(ExceptionContext filterContext)
        {
            var controllerName = (string)filterContext.RouteData.Values["controller"];
            var actionName = (string)filterContext.RouteData.Values["action"];

            var httpContextRequest = filterContext.HttpContext?.Request;
            var query = httpContextRequest?.Form ?? httpContextRequest?.QueryString;

            var queryString = string.Empty;

            if (query != null && query.Count > 0)
            {
                queryString = JsonConvert.SerializeObject(query);
            }

            var message =
                $"There was an error in controller '{controllerName}Controller' " +
                $"and action '{actionName}'{Environment.NewLine} " +
                $"Params: {queryString}{Environment.NewLine}" +
                $"Url: {httpContextRequest?.Url?.PathAndQuery}{Environment.NewLine}" +
                $"Referrer: {httpContextRequest?.UrlReferrer?.AbsolutePath}{Environment.NewLine}" +
                $"UserAgent: {httpContextRequest?.UserAgent}{Environment.NewLine}";


            if (!(filterContext.Exception is HttpException || filterContext.Exception is ILogUnhandledException))
            {
                Log.Error(message, filterContext.Exception);
                if (filterContext.Exception.InnerException != null)
                {
                    Log.Error(message, filterContext.Exception.InnerException);
                }
            }

            base.OnException(filterContext);
        }
    }
}