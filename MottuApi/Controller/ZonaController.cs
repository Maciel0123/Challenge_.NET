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

    /// <summary>
    /// Lista todas as zonas com seus relacionamentos.
    /// </summary>
    [HttpGet]
    public ActionResult<List<Zona>> ListarTodos()
    {
        return Ok(_service.ListarTodos());
    }

    /// <summary>
    /// Lista zonas com paginação.
    /// </summary>
    [HttpGet("paginado")]
    public ActionResult<List<Zona>> ListarPaginado(int page = 1, int pageSize = 10)
    {
        return Ok(_service.ListarPaginado(page, pageSize));
    }

    /// <summary>
    /// Lista as zonas de um determinado pátio.
    /// </summary>
    [HttpGet("patio/{patioId}")]
    public ActionResult<List<Zona>> ListarPorPatio(Guid patioId)
    {
        return Ok(_service.ListarPorPatio(patioId));
    }

    /// <summary>
    /// Retorna uma zona pelo seu ID.
    /// </summary>
    [HttpGet("{id}")]
    public ActionResult<Zona> ObterPorId(int id)
    {
        var zona = _service.ObterPorId(id);
        if (zona == null) return NotFound();

        return Ok(zona);
    }

    /// <summary>
    /// Cria uma nova zona.
    /// </summary>
    [HttpPost]
    public ActionResult<Zona> Criar(Zona zona)
    {
        var criada = _service.Criar(zona);
        return CreatedAtAction(nameof(ObterPorId), new { id = criada.Id }, criada);
    }

    /// <summary>
    /// Atualiza uma zona existente.
    /// </summary>
    [HttpPut]
    public IActionResult Atualizar(Zona zona)
    {
        var atualizado = _service.Atualizar(zona);
        if (!atualizado) return NotFound();

        return NoContent();
    }

    /// <summary>
    /// Remove uma zona pelo ID.
    /// </summary>
    [HttpDelete("{id}")]
    public IActionResult Remover(int id)
    {
        var removida = _service.Remover(id);
        if (!removida) return NotFound();

        return NoContent();
    }
}
