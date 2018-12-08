using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Features.DTO.Settings
{
    public class SettingsDTO
    {
        public ProfessionForSettingsDTO Profession { get; set; }
        
        public List<TimeSpan> PreferredTrainingTime { get; set; }
    }
}
