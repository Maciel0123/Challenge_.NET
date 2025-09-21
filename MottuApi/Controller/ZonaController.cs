// ZonaController.cs (completo com HATEOAS + paginação)
using Microsoft.AspNetCore.Mvc;
using MottuBusiness;
using MottuModel;

namespace MottuApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ZonaController : ControllerBase
{
    private readonly IZonaService _service;

    public ZonaController(IZonaService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult Get([FromQuery] Guid? patioId)
    {
        var zonas = patioId.HasValue ? _service.ListarPorPatio(patioId.Value) : _service.ListarTodos();
        return zonas.Any() ? Ok(zonas) : NoContent();
    }

    [HttpGet("paginado")]
    public IActionResult GetPaginado([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var zonas = _service.ListarPaginado(page, pageSize);
        return zonas.Count == 0 ? NoContent() : Ok(zonas);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var zona = _service.ObterPorId(id);
        if (zona == null) return NotFound();

        var response = new
        {
            zona.Id,
            zona.Nome,
            zona.PatioId,
            links = new[]
            {
                new { rel = "self", href = Url.Action(nameof(Get), new { id = zona.Id }), method = "GET" },
                new { rel = "update", href = Url.Action(nameof(Put)), method = "PUT" },
                new { rel = "delete", href = Url.Action(nameof(Delete), new { id = zona.Id }), method = "DELETE" }
            }
        };

        return Ok(response);
    }

    [HttpPost]
    public IActionResult Post([FromBody] Zona zona)
    {
        if (string.IsNullOrWhiteSpace(zona.Nome)) return BadRequest("Nome da zona é obrigatório.");
        var criada = _service.Criar(zona);
        return CreatedAtAction(nameof(Get), new { id = criada.Id }, criada);
    }

    [HttpPut]
    public IActionResult Put([FromBody] Zona zona)
    {
        var atualizada = _service.Atualizar(zona);
        return atualizada ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var removida = _service.Remover(id);
        return removida ? NoContent() : NotFound();
    }
}