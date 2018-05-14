using Services.DTO.Profession;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebUI.Controllers.API.Features
{
    //[Authorize]
    [RoutePrefix("api/professions")]
    public class ProfessionController : ApiController
    {
        private readonly IProfessionService _professionService;
        public ProfessionController(IProfessionService professionService)
        {
            _professionService = professionService;
        }
        [HttpGet]
        [Route("getAll")]
        public async Task<IHttpActionResult> GetAll()
        {
            var result = await _professionService.GetAllGrouped();
            return Ok(result.Select(f => {
                var dictionary = new Dictionary<string, List<ProfessionDTO>>();
                dictionary.Add(f.Key.ToUpper(), f.ToList());
                return dictionary;
            }));
        }
    }
}
