using Entity.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Implementations.Configurations
{
    public class ExerciseCriteriaConfiguration : ConfigurationBase<ExerciseCriteria>
    {
        public ExerciseCriteriaConfiguration() : base()
        {
            HasRequired(f => f.Excercise).WithMany(f => f.ExerciseCriterias).WillCascadeOnDelete(true);
            HasRequired(f => f.Criteria).WithMany().WillCascadeOnDelete(true);
        }
    }
}
