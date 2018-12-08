using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Error500()
        {

            HttpContext.Response.StatusCode = 500;
            return View();
        }
        public ActionResult Error404()
        {
            HttpContext.Response.StatusCode = 404;
            return View();
        }
        public ActionResult CustomError(Exception exception)
        {
            return View(exception);

        }

    }
}