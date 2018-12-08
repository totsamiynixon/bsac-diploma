using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Domain.Training
{
    public class UserExercise : IEntity
    {
        public int Id { get; set; }

        public UserTraining UserTraining { get; set; }

        public int UserTrainingId { get; set; }

        public Exercise Exercise { get; set; }

        public int ExerciseId { get; set; }

        public int CountOfRepeats { get; set; }
    }
}
