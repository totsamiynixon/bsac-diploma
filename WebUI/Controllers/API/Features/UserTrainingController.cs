using Microsoft.AspNet.Identity;
using Services.Features.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebUI.Models.Controllers.Features.UserTraining;

namespace WebUI.Controllers.API.Features
{
    [RoutePrefix("api/userTrainings")]
    public class UserTrainingController : ApiController
    {
        private readonly IUserTrainingService _userTrainingService;

        public UserTrainingController(IUserTrainingService userTrainingService)
        {
            _userTrainingService = userTrainingService;
        }

        [Route("get")]
        [HttpGet]
        public async Task<IHttpActionResult> GetUserTraining()
        {
            var userId = User.Identity.GetUserId<int>();
            var userTraining = await _userTrainingService.GenerateAndGetUserTrainingAsync(userId);
            return Ok(userTraining);
        }

        [Route("complete")]
        [HttpPost]
        public async Task<IHttpActionResult> CompleteUserTraining(CompeteUserTraining model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var userId = User.Identity.GetUserId<int>();
            await _userTrainingService.CompleteTraining(userId, model.UserTrainingId);
            return Ok();
        }
    }
}
