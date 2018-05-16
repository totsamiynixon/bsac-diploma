using Services.DTO.Criteria;
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
    //[Authorize(Roles = "Admin")]
    [RoutePrefix("api/admin/criteria")]
    public class AdminCriteriaController : ApiController
    {
        private readonly ICriteriaService _criteriaService;
        public AdminCriteriaController(ICriteriaService service)
        {
            _criteriaService = service;
        }
        /// <summary>
        /// Метод, кторый возвращает все критерии
        /// </summary>
        /// <returns> Все критерии </returns>
        [HttpGet]
        [Route("getAll")]
        public async Task<IHttpActionResult> GetAllAsync()
        {
            return Ok(await _criteriaService.GetAllAsync());
        }

        // GET: api/ApiCriteria/5
        [HttpGet]
        [Route("get")]
        public async Task<IHttpActionResult> GetAsync(int id)
        {
            return Ok(await _criteriaService.GetByIdAsync(id));
        }

        [HttpPost]
        [Route("save")]
        public async Task<IHttpActionResult> Save(CriteriaDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Model");
            }
            return Ok(await _criteriaService.AddOrUpdateCriteriaAsync(model));
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IHttpActionResult> Delete([FromUri]int[] ids)
        {
            await _criteriaService.DeleteAsync(ids);
            return Ok();
        }
    }
}
