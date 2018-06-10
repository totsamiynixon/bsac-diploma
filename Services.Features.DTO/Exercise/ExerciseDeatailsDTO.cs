using Services.Features.DTO.ExerciseCriteria;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Features.DTO.Exercise
{
    public class ExerciseDeatailsDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string VideoId { get; set; }

        public string PreviewText { get; set; }

        public string DifficultyLevel { get; set; }

        public List<ExerciseCriteriaDTO> Criterias { get; set; }
    }
}
