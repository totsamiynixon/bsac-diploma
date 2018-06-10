using Data.Interfaces;
using Data.Interfaces.Repositories;
using Entity.Domain;
using Entity.Domain.Settings;
using Services.Features.DTO.Settings;
using Services.Features.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace Services.Features.Implementations
{
    public class SettingsService : ISettingsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Settings> _settingsRepository;
        private readonly IGenericRepository<Profession> _professionRepository;
        private readonly IGenericRepository<TrainingTime> _trainingTimeRepostory;
        public SettingsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _settingsRepository = _unitOfWork.Repository<Settings>();
            _professionRepository = _unitOfWork.Repository<Profession>();
            _trainingTimeRepostory = _unitOfWork.Repository<TrainingTime>();
        }

        public async Task<List<ProfessionForSettingsDTO>> GetProfessionsForSettingsAsync()
        {
            return await _professionRepository.Collection.Select(s => new ProfessionForSettingsDTO
            {
                Id = s.Id,
                Name = s.Name
            }).ToListAsync();
        }

        public async Task<SettingsDTO> GetSettingsAsync(int userId)
        {
            var result = await _settingsRepository.Collection.Include(s => s.Profession).Include(s => s.DefaultTrainingTimes).FirstOrDefaultAsync(s => s.Id == userId);
            if (result != null)
            {
                return new SettingsDTO
                {
                    Profession = result.Profession != null ? new ProfessionForSettingsDTO
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
            foreach(var tt in settings.DefaultTrainingTimes.ToArray())
            {
                _trainingTimeRepostory.Delete(tt);
            }
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
