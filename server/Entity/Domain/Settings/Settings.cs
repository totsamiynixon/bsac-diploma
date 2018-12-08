using Contracts;
using Entity.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Domain.Settings
{
    public class Settings : IEntity
    {
        public int Id { get; set; }

        public User User { get; set; }

        public Profession Profession { get; set; }

        public int? ProfessionId { get; set; }

        public List<TrainingTime> DefaultTrainingTimes { get; set; }
       
    }
}
