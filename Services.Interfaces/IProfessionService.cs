using Services.DTO.Profession;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IProfessionService
    {
        Task<int> AddOrUpdateProfessionAsync(ProfessionDetailsDTO dto);
        Task DeleteAsync(params int[] ids);
        Task<List<ProfessionDTO>> GetAllAsync();
        Task<ProfessionDetailsDTO> GetByIdAsync(int id);
    }
}
