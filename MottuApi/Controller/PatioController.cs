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
public class PatioController : ControllerBase
{
    private readonly IPatioService _service;

    public PatioController(IPatioService service)
    {
        _service = service;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<Patio>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<List<Patio>> ListarTodos()
        => Ok(_service.ListarTodos());

    [HttpGet("paginado")]
    [ProducesResponseType(typeof(List<Patio>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<List<Patio>> ListarPaginado(int page = 1, int pageSize = 10)
        => Ok(_service.ListarPaginado(page, pageSize));

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<object> ObterPorId(Guid id)
    {
        var patio = _service.ObterPorId(id);
        if (patio == null) return NotFound();

        var self = Url.Action(nameof(ObterPorId), new { id, version = "1" });
        var update = Url.Action(nameof(Atualizar), new { version = "1" });
        var delete = Url.Action(nameof(Remover), new { id, version = "1" });

        return Ok(new
        {
            data = patio,
            links = new[]
            {
                new { rel = "self",   href = self,   method = "GET" },
                new { rel = "update", href = update, method = "PUT" },
                new { rel = "delete", href = delete, method = "DELETE" }
            }
        });
    }

    [HttpPost]
    [ProducesResponseType(typeof(Patio), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<Patio> Criar(Patio patio)
    {
        var criado = _service.Criar(patio);
        return CreatedAtAction(nameof(ObterPorId), new { id = criado.Id, version="1" }, criado);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult Atualizar(Patio patio)
    {
        var atualizado = _service.Atualizar(patio);
        if (!atualizado) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult Remover(Guid id)
    {
        var removido = _service.Remover(id);
        if (!removido) return NotFound();
        return NoContent();
    }
}
