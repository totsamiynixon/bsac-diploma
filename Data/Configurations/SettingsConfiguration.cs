using Entity.Domain.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Implementations.Configurations
{
    public class SettingsConfiguration : EntityTypeConfiguration<Settings>
    {
        public SettingsConfiguration()
        {
            HasKey(s => s.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            //HasRequired(s => s.User).WithOptional(s => s.Settings)
            HasOptional(s => s.Profession).WithMany().WillCascadeOnDelete(true);
            HasMany(s => s.DefaultTrainingTimes).WithRequired(z => z.Settings);
        }
    }
}
