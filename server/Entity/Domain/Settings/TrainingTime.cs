using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Domain.Settings
{
    public class TrainingTime : IIdEntity
    {
        public int Id { get; set; }

        public TimeSpan Value { get; set; }

        public Settings Settings { get; set; }

        public int SettingsId { get; set; }
    }
}
