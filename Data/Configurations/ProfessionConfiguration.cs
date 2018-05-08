using Entity.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Implementations.Configurations
{
    public class ProfessionConfiguration : ConfigurationBase<Profession>
    {
        public ProfessionConfiguration() : base()
        {
            HasMany(f => f.ProfessionCriterias).WithRequired(f => f.Profession).WillCascadeOnDelete(true);
        }
    }
}
