using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Features.Models
{
    public class SearchExerciseModel
    {
        public string Query { get; set; }

        public bool ByExercises { get; set; }

        public bool ByCriterias { get; set; }

    }
}
