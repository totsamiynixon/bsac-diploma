using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Domain
{
    public class ExerciseCriteria : IIdEntity
    {
        public int Id { get; set; }
        public int Weight { get; set; }
        public Exercise Exercise { get; set; }
        public int ExerciseId { get; set; }
        public Criteria Criteria { get; set; }
        public int CriteriaId { get; set; }
    }
}
