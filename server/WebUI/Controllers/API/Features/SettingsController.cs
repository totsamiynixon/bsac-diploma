using Microsoft.AspNet.Identity;
using Services.Features.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebUI.Models.Controllers.Features.Settings;

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

        [HttpPost]
        [Route("saveProfession")]
        public async Task<IHttpActionResult> ChangeProfession(SetProfessionModel model)
        {
            var userId = User.Identity.GetUserId<int>();
            await _settingsService.UpdateProfessionAsync(userId, model.ProfessionId);
            return Ok();
        }

        [HttpPost]
        [Route("savePrefferedTime")]
        public async Task<IHttpActionResult> ChangePreferredTime(List<TimeSpan> preferredTimes)
        {
            var userId = User.Identity.GetUserId<int>();
            await _settingsService.UpdateDefaultTrainingTimesAsync(userId, preferredTimes);
            return Ok();
        }
    }
}
