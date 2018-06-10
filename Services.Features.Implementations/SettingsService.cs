using Data.Interfaces;
using Data.Interfaces.Repositories;
using Entity.Domain.Settings;
using Services.DTO.Settings;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace Services
{
    public class SettingsService : ISettingsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Settings> _settingsRepository;
        public SettingsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _settingsRepository = _unitOfWork.Repository<Settings>();
        }

        public async Task<SettingsDTO> GetSettingsAsync(int userId)
        {
            var result =  await _settingsRepository.Collection.Include(s=>s.Profession).Include(s=>s.DefaultTrainingTimes).FirstOrDefaultAsync(s => s.Id == userId);
            if(result != null)
            {
                return new SettingsDTO
                {
                    Profession = result.Profession != null ? new SettingsProfessionDTO
                    {
                        Id = result.Profession.Id,
                        Name = result.Profession.Name
                    } : null,
                    PreferredTrainingTime = result.DefaultTrainingTimes.Select(s => s.Value).ToList()
                };
            }
            return null;
        }

        public async Task UpdateDefaultTrainingTimesAsync(int userId, List<TimeSpan> times)
        {
            var settings = await _settingsRepository.CollectionWithTracking.Include(s => s.DefaultTrainingTimes).FirstOrDefaultAsync(s => s.Id == userId);
            if (settings == null)
            {
                return;
            }
            settings.DefaultTrainingTimes.Clear();
            var timesToAdd = times.Select(z => new TrainingTime
            {
                Value = z
            }).ToList();
            settings.DefaultTrainingTimes.AddRange(timesToAdd);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateProfessionAsync(int userId, int professionId)
        {
            await _settingsRepository.Collection.Where(s => s.Id == userId).UpdateAsync(z => new Settings
            {
                ProfessionId = professionId
            });
        }
    }
}
