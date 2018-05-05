using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebUI.Identity;

namespace WebUI.Controllers.API
{
    public class TestController : ApiController
    {
        private ApplicationUserManager _userManager;

        public TestController(ApplicationUserManager userManager)
        {
            _userManager = userManager;
        }

        public IHttpActionResult GetAllUsers()
        {
            throw new Exception();
            var users = _userManager.Users.ToList();
            return Ok(users);
        }
    }
}
