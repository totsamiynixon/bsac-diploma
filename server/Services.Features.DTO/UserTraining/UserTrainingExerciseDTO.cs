using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Features.DTO.UserTraining
{
    public class UserTrainingExerciseDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string PreviewText { get; set; }

        public string VideoId { get; set; }

        public string CountOfRepeats { get; set; }
    }
}
