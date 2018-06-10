using Contracts;
using Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Domain
{
    public class Exercise : IIdEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string PreviewText { get; set; }

        public string VideoUrl { get; set; }

        public DifficultyLevel DifficultyLevel { get; set; }

        public List<ExerciseCriteria> ExerciseCriterias { get; set; }
    }
}
