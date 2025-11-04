using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MottuBusiness;
using MottuModel;

namespace MottuApi.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize]
[Produces("application/json")]
public class ZonaController : ControllerBase
{
    private readonly IZonaService _service;

    public ZonaController(IZonaService service)
    {
        _service = service;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<Zona>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<List<Zona>> ListarTodos()
        => Ok(_service.ListarTodos());

    [HttpGet("paginado")]
    [ProducesResponseType(typeof(List<Zona>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<List<Zona>> ListarPaginado(int page = 1, int pageSize = 10)
        => Ok(_service.ListarPaginado(page, pageSize));

    [HttpGet("patio/{patioId}")]
    [ProducesResponseType(typeof(List<Zona>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<List<Zona>> ListarPorPatio(Guid patioId)
        => Ok(_service.ListarPorPatio(patioId));

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<object> ObterPorId(int id)
    {
        var zona = _service.ObterPorId(id);
        if (zona == null) return NotFound();

        var self = Url.Action(nameof(ObterPorId), new { id, version = "1" });
        var update = Url.Action(nameof(Atualizar), new { version = "1" });
        var delete = Url.Action(nameof(Remover), new { id, version = "1" });

        return Ok(new
        {
            data = zona,
            links = new[]
            {
                new { rel = "self",   href = self,   method = "GET" },
                new { rel = "update", href = update, method = "PUT" },
                new { rel = "delete", href = delete, method = "DELETE" }
            }
        });
    }

    [HttpPost]
    [ProducesResponseType(typeof(Zona), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<Zona> Criar(Zona zona)
    {
        var criada = _service.Criar(zona);
        return CreatedAtAction(nameof(ObterPorId), new { id = criada.Id, version="1" }, criada);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult Atualizar(Zona zona)
    {
        var atualizado = _service.Atualizar(zona);
        if (!atualizado) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult Remover(int id)
    {
        var removida = _service.Remover(id);
        if (!removida) return NotFound();
        return NoContent();
    }
}
