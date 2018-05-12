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
            return await _settingsRepository.Collection.Where(s => s.Id == userId).Select(s => new SettingsDTO
            {
                Profession = new SettingsProfessionDTO
                {
                    Id = s.Profession.Id,
                    Name = s.Profession.Name
                },
                SettingsTrainingTime = s.DefaultTrainingTimes.Select(r => r.Value).ToList()
            }).FirstOrDefaultAsync();
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
