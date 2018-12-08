using Services.DTO.Exercise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IExerciseService
    {
        Task<int> AddOrUpdateExerciseAsync(ExerciseDetailsDTO dto);
        Task DeleteAsync(params int[] ids);
        Task<List<ExerciseDTO>> GetAllAsync();
        Task<ExerciseDetailsDTO> GetByIdAsync(int id);
    }
}
