
using Services.Features.DTO.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Features.Interfaces
{
    public interface ISettingsService
    {
        Task UpdateProfessionAsync(int userId, int professionId);
        Task UpdateDefaultTrainingTimesAsync(int userId, List<TimeSpan> times);
        Task<SettingsDTO> GetSettingsAsync(int userId);
        Task<List<ProfessionForSettingsDTO>> GetProfessionsForSettingsAsync();
    }
}
