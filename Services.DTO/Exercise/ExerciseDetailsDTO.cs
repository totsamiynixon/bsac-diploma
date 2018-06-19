using Entity.Enums;
using Services.DTO.ExerciseCriteria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO.Exercise
{
    public class ExerciseDetailsDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string VideoId { get; set; }

        public string PreviewText { get; set; }

        public DifficultyLevel DifficultyLevel { get; set; }

        public List<ExerciseCriteriaDTO> Criterias { get; set; }

    }
}
