using Services.Features.DTO.ExerciseCriteria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Features.DTO.Exercise
{
    public class ExerciseDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string VideoId { get; set; }

        public List<ExerciseCriteriaDTO> Criterias { get; set; }

    }
}
