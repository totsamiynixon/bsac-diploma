using Services.DTO.Exercise;
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
        public ExerciseController(IExerciseService service)
        {
            _exerciseService = service;
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
        public async Task<IHttpActionResult> GetAsync(int id)
        {
            return Ok(await _exerciseService.GetByIdAsync(id));
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
