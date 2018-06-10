using Services.DTO.Criteria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICriteriaService
    {
        Task<int> AddOrUpdateCriteriaAsync(CriteriaDTO dto);
        Task DeleteAsync(params int[] ids);
        Task<List<CriteriaDTO>> GetAllAsync();
        Task<CriteriaDTO> GetByIdAsync(int id);
    }
}
