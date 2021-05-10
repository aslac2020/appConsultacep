using appConsutaCepApi.Models;
using appConsutaCepApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace appConsutaCepApi.Controllers
{
    [Route("api/[controller]")]
    public class CepController : ControllerBase
    {
        private readonly CepServices _services;
        private readonly ICepServices _repositories;
        private readonly ILogger<CepController> _logger;

        public CepController(CepServices services, ICepServices cepServices, ILogger<CepController> logger)
        {
            _services = services;
            _logger = logger;
            _repositories = cepServices;
        }

        [HttpGet]
        public async Task<IActionResult> SearchCep(string cep)
        {
            if (cep == null)
            {
                return BadRequest("Ops! Cep não encontrado :(");
            }

            return Ok(await _services.SearchCep(cep));
        }
    }
}
