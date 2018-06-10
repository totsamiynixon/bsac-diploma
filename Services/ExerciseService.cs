using AutoMapper;
using Data.Interfaces;
using Data.Interfaces.Repositories;
using Entity.Domain;
using Services.DTO.Exercise;
using Services.DTO.ExerciseCriteria;
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
    public class ExerciseService : IExerciseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Exercise> _exerciseRepository;
        private readonly IGenericRepository<ExerciseCriteria> _exerciseCriteriaRepository;
        public ExerciseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _exerciseRepository = _unitOfWork.Repository<Exercise>();
            _exerciseCriteriaRepository = _unitOfWork.Repository<ExerciseCriteria>();
        }
        public async Task<int> AddOrUpdateExerciseAsync(ExerciseDetailsDTO dto)
        {
            var exercise = Mapper.Map<ExerciseDetailsDTO, Exercise>(dto);
            var exerciseInDb = await _exerciseRepository.CollectionWithTracking.Include(z => z.ExerciseCriterias).FirstOrDefaultAsync(f => f.Id == exercise.Id);
            if (exerciseInDb == null)
            {
                _exerciseRepository.Insert(exercise);
            }
            else
            {
                Mapper.Map(exercise, exerciseInDb);
                _exerciseCriteriaRepository.Delete(exerciseInDb.ExerciseCriterias.ToArray());
                exerciseInDb.ExerciseCriterias.AddRange(exercise.ExerciseCriterias);
                _exerciseRepository.Update(exerciseInDb);
            }
            await _unitOfWork.SaveChangesAsync();
            return exercise.Id;
        }

        public async Task DeleteAsync(params int[] ids)
        {
            await _exerciseRepository.Collection.Where(f => ids.Any(z => z == f.Id)).DeleteAsync();
        }

        public async Task<List<ExerciseDTO>> GetAllAsync()
        {
            return await _exerciseRepository.Collection.Select(f => new ExerciseDTO
            {
                Id = f.Id,
                Name = f.Name,
                VideoId = f.VideoId
            }).ToListAsync();
        }

        public async Task<ExerciseDetailsDTO> GetByIdAsync(int id)
        {
            var exercise = await _exerciseRepository.Collection.Include(f => f.ExerciseCriterias).FirstOrDefaultAsync(f => f.Id == id);
            return Mapper.Map<Exercise, ExerciseDetailsDTO>(exercise);
        }
    }
}
