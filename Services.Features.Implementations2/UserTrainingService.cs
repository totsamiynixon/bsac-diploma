using AutoMapper;
using Data.Interfaces;
using Data.Interfaces.Repositories;
using Entity.Domain;
using Entity.Domain.Settings;
using Entity.Domain.Training;
using Services.Features.DTO.UserTraining;
using Services.Features.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace Services.Features.Implementations2
{
    public class UserTrainingService : IUserTrainingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<UserTraining> _userTrainingRepository;
        private readonly IGenericRepository<Exercise> _exercisesRepository;
        private readonly IGenericRepository<Profession> _professionRepository;
        private readonly IGenericRepository<ExerciseCriteria> _exerciseCriteriaRepository;
        private readonly IGenericRepository<ProfessionCriteria> _professionCriteriaRepository;
        private readonly IGenericRepository<Settings> _settingsRepository;

        public UserTrainingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userTrainingRepository = _unitOfWork.Repository<UserTraining>();
            _exercisesRepository = _unitOfWork.Repository<Exercise>();
            _professionRepository = _unitOfWork.Repository<Profession>();
            _exerciseCriteriaRepository = _unitOfWork.Repository<ExerciseCriteria>();
            _professionCriteriaRepository = _unitOfWork.Repository<ProfessionCriteria>();
            _settingsRepository = _unitOfWork.Repository<Settings>();
        }
        public async Task CompleteTraining(int userId, int trainingId)
        {
           await _userTrainingRepository.Collection.Where(s => s.UserId == userId && s.Id == trainingId).UpdateAsync(s => new UserTraining
            {
                IsPassed = true
            });
        }

        public async Task<UserTrainingDTO> GenerateAndGetUserTrainingAsync(int userId)
        {
            var userProfessionId = await _settingsRepository.Collection.Where(s => s.Id == userId).Select(f => f.ProfessionId).FirstOrDefaultAsync();
            if (!userProfessionId.HasValue)
            {
                throw new Exception("User has no setted up profession! Please, set up profession first!");
            }
            var criteriasIds = await _professionCriteriaRepository.Collection.OrderByDescending(s => s.Weight).Take(5).Select(s => s.CriteriaId).ToListAsync();
            var exercises = await _exerciseCriteriaRepository.Collection.Where(s => criteriasIds.Any(z => z == s.CriteriaId)).OrderByDescending(f => f.Weight).Take(5).Select(s => s.Exercise).ToListAsync();
            var userTraining = new UserTraining
            {
                Created = DateTime.UtcNow,
                UserId = userId,
                Exercises = exercises.Select(ex => new UserExercise
                {
                    ExerciseId = ex.Id,
                    CountOfRepeats = 10
                }).ToList()
            };
            _userTrainingRepository.Insert(userTraining);
            await _unitOfWork.SaveChangesAsync();
            return Mapper.Map<UserTraining, UserTrainingDTO>(userTraining);
        }
    }
}
