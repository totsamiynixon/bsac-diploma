using Services.DTO.ProfessionCriteria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO.Profession
{
    public class ProfessionDetailsDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<ProfessionCriteriaDTO> Criterias { get; set; }

    }
}
