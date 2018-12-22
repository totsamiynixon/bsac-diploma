using Contracts;
using Entity.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Domain.Training
{
    public class UserTraining: IEntity
    {
        public int Id { get; set; }

        public User User { get; set; }

        public int UserId { get; set; }

        public DateTime Created { get; set; }

        public bool IsPassed { get; set; }

        public List<UserExercise> Exercises { get; set; }

        public int? Rating { get; set; }
    }
}
