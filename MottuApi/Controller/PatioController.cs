using Microsoft.AspNetCore.Mvc;
using MottuBusiness;
using MottuModel;

namespace MottuApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatioController : ControllerBase
{
    private readonly IPatioService _service;

    public PatioController(IPatioService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult Get() => Ok(_service.ListarTodos());

    [HttpGet("paginado")]
    public IActionResult GetPaginado([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var patios = _service.ListarPaginado(page, pageSize);
        return patios.Count == 0 ? NoContent() : Ok(patios);
    }

    [HttpGet("{id}")]
    public IActionResult Get(Guid id)
    {
        var patio = _service.ObterPorId(id);
        if (patio == null) return NotFound();

        var response = new
        {
            patio.Id,
            patio.Nome,
            links = new[]
            {
                new { rel = "self", href = Url.Action(nameof(Get), new { id = patio.Id }), method = "GET" },
                new { rel = "update", href = Url.Action(nameof(Put)), method = "PUT" },
                new { rel = "delete", href = Url.Action(nameof(Delete), new { id = patio.Id }), method = "DELETE" }
            }
        };

        return Ok(response);
    }

    [HttpPost]
    public IActionResult Post([FromBody] Patio patio)
    {
        var criado = _service.Criar(patio);
        return CreatedAtAction(nameof(Get), new { id = criado.Id }, criado);
    }

    [HttpPut]
    public IActionResult Put([FromBody] Patio patio)
    {
        var atualizado = _service.Atualizar(patio);
        return atualizado ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        var removido = _service.Remover(id);
        return removido ? NoContent() : NotFound();
    }
}
