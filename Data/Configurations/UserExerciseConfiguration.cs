using Entity.Domain.Training;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Implementations.Configurations
{
    public class UserExerciseConfiguration : ConfigurationBase<UserExercise>
    {
        public UserExerciseConfiguration()
        {
            HasRequired(s => s.Exercise).WithOptional().WillCascadeOnDelete(true);
        }
    }
}
