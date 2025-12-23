using Cliente.Aplicacion.DTO.RequestResponse;
using Cliente.Aplicacion.Servicios.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cliente.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _service;

        public ClientesController(IClienteService service) {
            _service = service;
        }

        [HttpGet("{identificacion}")]
        public async Task<IActionResult> ObtenerInfoCliente(string identificacion)
        {
            ResponseDTO response = await _service.ObtenerInfoCliente(identificacion);
            if (response.IsValid)
            {
                return Ok(response);
            }
            else {
                return NotFound(response);
            }            
        }

        
        
    }
}
