using Services.Features.DTO.Exercise;
using Services.Features.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Features.Interfaces
{
    public interface IExerciseService
    {
        Task<ExerciseDTO[]> GetAllExercisesAsync(SearchExerciseModel searchModel);
        Task<ExerciseDeatailsDTO> GetSingleExerciseAsync(int id);
    }
}
