using Services.Features.DTO.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models.Controllers.Features.Settings
{
    public class InitDataModel
    {
        public SettingsDTO  Settings { get; set; }

        public Dictionary<string, List<ProfessionForSettingsDTO>> Professions { get; set; }
    }
}