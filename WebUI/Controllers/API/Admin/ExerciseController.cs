using Services.DTO.Exercise;
using Services.DTO.ExerciseCriteria;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebUI.Controllers.API.Admin
{
    [RoutePrefix("api/admin/exercise")]
    public class ExerciseController : ApiController
    {
        private readonly IExerciseService _exerciseService;
        private readonly ICriteriaService _criteriaService;
        public ExerciseController(IExerciseService service, ICriteriaService criteriaService)
        {
            _exerciseService = service;
            _criteriaService = criteriaService;
        }
        // GET: api/ApiExercise
        [HttpGet]
        [Route("getAll")]
        public async Task<IHttpActionResult> GetAllAsync()
        {
            return Ok(await _exerciseService.GetAllAsync());
        }

        // GET: api/ApiExercise/5
        [HttpGet]
        [Route("get")]
        public async Task<IHttpActionResult> GetAsync(int? id = null)
        {
            var criterias = await _criteriaService.GetAllAsync();
            if (id.HasValue)
            {
                return Ok(new {
                    exercise = await _exerciseService.GetByIdAsync(id.Value),
                    criterias = criterias
                });
            }
            else
            {
                return Ok(new {
                    exercise = new ExerciseDetailsDTO
                    {
                        Criterias = new List<ExerciseCriteriaDTO>()
                    },
                    criterias = criterias
                });
            }
        }


        [HttpPost]
        [Route("save")]
        public async Task<IHttpActionResult> Save(ExerciseDetailsDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Model");
            }
            return Ok(await _exerciseService.AddOrUpdateExerciseAsync(model));
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IHttpActionResult> Delete(int[] ids)
        {
            await _exerciseService.DeleteAsync(ids);
            return Ok();
        }
    }
}
