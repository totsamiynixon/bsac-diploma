using Entity.Domain.Training;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Implementations.Configurations
{
    public class UserTrainingConfiguration : ConfigurationBase<UserTraining>
    {
        public UserTrainingConfiguration()
        {
            HasMany(s => s.Exercises).WithRequired(s => s.UserTraining).HasForeignKey(s=>s.UserTrainingId).WillCascadeOnDelete(true);
        }
    }
}
