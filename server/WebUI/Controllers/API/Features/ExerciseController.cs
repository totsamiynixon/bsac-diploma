using Services.Features.Interfaces;
using Services.Features.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebUI.Controllers.API.Features
{
    [RoutePrefix("api/exercises")]
    public class ExerciseController : ApiController
    {
        private readonly IExerciseService _exerciseService;
        public ExerciseController(IExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }

        [Route("getAll")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllAsync([FromUri] SearchExerciseModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if(model == null)
            {
                model = new SearchExerciseModel();
            }
            var exercises = await _exerciseService.GetAllExercisesAsync(model);
            return Ok(exercises);
        }

        [Route("get")]
        public async Task<IHttpActionResult> GetSingleAsync(int id)
        {
            var exercise = await _exerciseService.GetSingleExerciseAsync(id);
            return Ok(exercise);
        }
    }
}
