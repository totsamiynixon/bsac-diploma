
using Servces.Features.DTO.Exercise;
using Services.DTO.Features.Exercise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Features.Interfaces
{
    public interface IExerciseService
    {
        Task<ExerciseDTO[]> GetAllExercises(string query, int[] criteriaIds, int[] professionIds);
        Task<ExerciseDeatailsDTO> GetSingleExercise(int id);
    }
}
