using Entity.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Implementations.Configurations
{
    public class ExerciseConfiguration: ConfigurationBase<Exercise>
    {
        public ExerciseConfiguration() : base()
        {
            HasMany(f => f.ExerciseCriterias).WithRequired(f => f.Excercise).WillCascadeOnDelete(true);
        }
    }
}
