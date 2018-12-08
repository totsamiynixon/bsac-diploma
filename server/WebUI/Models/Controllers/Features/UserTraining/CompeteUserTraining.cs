using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebUI.Models.Controllers.Features.UserTraining
{
    public class CompeteUserTraining
    {
        [Required]
        public int UserTrainingId { get; set; }
    }
}