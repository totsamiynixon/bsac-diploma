﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Features.DTO.UserTraining
{
    public class UserTrainingDTO
    {
        public int Id { get; set; }

        public DateTime Created { get; set; }

        public List<UserTrainingExerciseDTO> Exercises { get; set; }

        public bool IsPassed { get; set; }
    }
}
