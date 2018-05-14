using Microsoft.AspNet.Identity;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebUI.Controllers.API.Mobile
{
    [RoutePrefix("api/settings")]
    [Authorize]
    public class SettingsController : ApiController
    {
        private readonly ISettingsService _settingsService;
        public SettingsController(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetSettings()
        {
            var userId = User.Identity.GetUserId<int>();
            var settings = await _settingsService.GetSettingsAsync(userId);
            return Ok(settings);
        }

        [HttpPost]
        [Route("profession")]
        public async Task<IHttpActionResult> ChangeProfession(int professionId)
        {
            var userId = User.Identity.GetUserId<int>();
            await _settingsService.UpdateProfessionAsync(userId, professionId);
            return Ok();
        }

        [HttpPost]
        [Route("preferredTimes")]
        public async Task<IHttpActionResult> ChangePreferredTime(List<TimeSpan> preferredTimes)
        {
            var userId = User.Identity.GetUserId<int>();
            await _settingsService.UpdateDefaultTrainingTimesAsync(userId, preferredTimes);
            return Ok();
        }
    }
}
