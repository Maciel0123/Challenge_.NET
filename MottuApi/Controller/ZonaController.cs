using Microsoft.AspNetCore.Mvc;
using MottuBusiness;
using MottuModel;

namespace MottuApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ZonaController : ControllerBase
    {
        private readonly IZonaService _service;

        public ZonaController(IZonaService service)
        {
            _service = service;
        }

        // 🔹 GET com filtro opcional por PatioId
        [HttpGet]
        public IActionResult Get([FromQuery] Guid? patioId)
        {
            if (patioId.HasValue)
            {
                var zonasFiltradas = _service.ListarPorPatio(patioId.Value);
                return zonasFiltradas.Any() ? Ok(zonasFiltradas) : NoContent();
            }

            var zonas = _service.ListarTodos();
            return zonas.Any() ? Ok(zonas) : NoContent();
        }

        // 🔹 GET por ID da zona
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var zona = _service.ObterPorId(id);
            return zona == null ? NotFound() : Ok(zona);
        }

        // 🔹 POST zona
        [HttpPost]
        public IActionResult Post([FromBody] Zona zona)
        {
            if (string.IsNullOrWhiteSpace(zona.Nome))
                return BadRequest("Nome da zona é obrigatório.");

            var criada = _service.Criar(zona);
            return CreatedAtAction(nameof(Get), new { id = criada.Id }, criada);
        }
    }
}
