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
    [Authorize]
    [RoutePrefix("api/usertrainings")]
    public class UserTrainingController : ApiController
    {
        private readonly IUserTrainingService _userTrainingService;

        public UserTrainingController(IUserTrainingService userTrainingService)
        {
            _userTrainingService = userTrainingService;
        }

        [Route("get/grouped")]
        [HttpGet]
        public async Task<IHttpActionResult> GetUserTrainingGroupedAsync()
        {
            var userId = User.Identity.GetUserId<int>();
            var userTraining = await _userTrainingService.GenerateAndGetUserTrainingAsync(userId);
            return Ok(userTraining);
        }

        [Route("get")]
        [HttpGet]
        public async Task<IHttpActionResult> GetUserTrainingAsync()
        {
            var userId = User.Identity.GetUserId<int>();
            var userTraining = await _userTrainingService.GetLastUserTrainingAsync(userId);
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
            await _userTrainingService.CompleteTrainingAsync(userId, model.UserTrainingId);
            return Ok();
        }

        [Route("rating")]
        [HttpPost]
        public async Task<IHttpActionResult> UpdateUserTrainingRating(UpdateUserTrainingRating model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var userId = User.Identity.GetUserId<int>();
            await _userTrainingService.UpdateTrainingRatingAsync(userId, model.UserTrainingId, model.Rating);
            return Ok();
        }
    }
}
