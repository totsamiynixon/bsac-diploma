using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO.Settings
{
    public class SettingsDTO
    {
        public SettingsProfessionDTO Profession { get; set; }
        
        public List<TimeSpan> SettingsTrainingTime { get; set; }
    }
}
