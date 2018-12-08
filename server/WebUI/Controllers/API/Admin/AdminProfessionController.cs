using Services.DTO.Profession;
using Services.DTO.ProfessionCriteria;
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
    [RoutePrefix("api/admin/profession")]
    public class AdminProfessionController : ApiController
    {
        private readonly IProfessionService _professionService;
        private readonly ICriteriaService _criteriaService;
        public AdminProfessionController(IProfessionService service, ICriteriaService criteriaService)
        {
            _professionService = service;
            _criteriaService = criteriaService;
        }
        // GET: api/ApiProfession
        [HttpGet]
        [Route("getAll")]
        public async Task<IHttpActionResult> GetAllAsync()
        {
            return Ok(await _professionService.GetAllAsync());
        }

        // GET: api/ApiProfession/5
        [HttpGet]
        [Route("get")]
        public async Task<IHttpActionResult> GetAsync(int? id = null)
        {
            var criterias = await _criteriaService.GetAllAsync();
            if (id.HasValue)
            {
                return Ok(new
                {
                    profession = await _professionService.GetByIdAsync(id.Value),
                    criterias = criterias
                });
            }
            else
            {
                return Ok(new
                {
                    profession = new ProfessionDetailsDTO
                    {
                        Criterias = new List<ProfessionCriteriaDTO>()
                    },
                    criterias = criterias
                });
            }
        }


        [HttpPost]
        [Route("save")]
        public async Task<IHttpActionResult> Save(ProfessionDetailsDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Model");
            }
            return Ok(await _professionService.AddOrUpdateProfessionAsync(model));
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IHttpActionResult> Delete([FromUri]int[] ids)
        {
            await _professionService.DeleteAsync(ids);
            return Ok();
        }
    }
}
