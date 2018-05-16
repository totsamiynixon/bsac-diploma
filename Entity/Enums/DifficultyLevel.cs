using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Enums
{
    public enum DifficultyLevel
    {
       [Description("Легко")]
       Easy,
       [Description("Средне")]
       Medium,
       [Description("Тяжело")]
       Hard
    }
}
