using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Features.DTO.UserTraining
{
    public class UserTrainingGroupedDTO
    {
        public int Id { get; set; }

        public DateTime Created { get; set; }

        public Dictionary<string, List<UserTrainingExerciseDTO>> Exercises { get; set; }

        public bool IsPassed { get; set; }
    }
}
