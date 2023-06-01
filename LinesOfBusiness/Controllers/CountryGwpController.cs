using LinesOfBusiness.DTO;
using LinesOfBusiness.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LinesOfBusiness.Controllers
{
    [Route("server/api/gwp")]
    [ApiController]
    public class CountryGwpController : ControllerBase
    {
        private readonly IGwpRepository repository;

        public CountryGwpController(IGwpRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost("avg")]
        public async Task<ActionResult<IDictionary<string, decimal>>> GetGwps(GwpRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }
            var output = await repository.GetGwps(request.Country, request.Lob);
            return Ok(output);
        }
    }
}
