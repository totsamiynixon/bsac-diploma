
using AutoMapper;
using Data.Interfaces;
using Data.Interfaces.Repositories;
using Entity.Domain;
using LinqKit;
using Services.Features.DTO.Exercise;
using Services.Features.Interfaces;
using Services.Features.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Features.Implementations
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

        public async Task<ExerciseDTO[]> GetAllExercisesAsync(SearchExerciseModel searchModel)
        {
            var exercies = await _exerciseRepository.Collection.Include(s=>s.ExerciseCriterias).Where(BuildSearchExpression(searchModel)).ToArrayAsync();
            return Mapper.Map<Exercise[], ExerciseDTO[]>(exercies);
        }

        public async Task<ExerciseDeatailsDTO> GetSingleExerciseAsync(int id)
        {
            var exercise = await _exerciseRepository.Collection.FirstOrDefaultAsync(s => s.Id == id);
            return Mapper.Map<Exercise, ExerciseDeatailsDTO>(exercise);
        }


        private Expression<Func<Exercise, bool>> BuildSearchExpression(SearchExerciseModel searchModel)
        {
            var predicate = PredicateBuilder.New<Exercise>();
            if (string.IsNullOrEmpty(searchModel.Query))
            {
                return PredicateBuilder.True<Exercise>();
            }
            if (searchModel.ByExercises)
            {
                predicate = predicate.Or(s => s.Description.Contains(searchModel.Query) || s.Name.Contains(searchModel.Query) || s.PreviewText.Contains(searchModel.Query));
            }
            if (searchModel.ByCriterias)
            {
                predicate = predicate.Or(s => s.ExerciseCriterias.Any(z => z.Criteria.Name.Contains(searchModel.Query)));
            }
            return predicate;
        }
    }
}
