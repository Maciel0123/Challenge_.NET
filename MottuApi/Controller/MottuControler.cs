using Microsoft.AspNetCore.Mvc;
using MottuBusiness;
using MottuModel;

namespace MottuApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MotoController : ControllerBase
{
    private readonly IMottuService _service;

    public MotoController(IMottuService service)
    {
        _service = service;
    }

    /// <summary>
    /// Lista todas as motos.
    /// </summary>
    [HttpGet]
    public ActionResult<List<Moto>> ListarTodos()
    {
        return Ok(_service.ListarTodos());
    }

    /// <summary>
    /// Lista motos com paginação.
    /// </summary>
    [HttpGet("paginado")]
    public ActionResult<List<Moto>> ListarPaginado(int page = 1, int pageSize = 10)
    {
        return Ok(_service.ListarPaginado(page, pageSize));
    }

    /// <summary>
    /// Retorna uma moto por ID.
    /// </summary>
    [HttpGet("{id}")]
    public ActionResult<Moto> ObterPorId(string id)
    {
        var moto = _service.ObterPorId(id);
        if (moto == null) return NotFound();

        return Ok(moto);
    }

    /// <summary>
    /// Cria uma nova moto.
    /// </summary>
    [HttpPost]
    public ActionResult<Moto> Criar(Moto moto)
    {
        var criada = _service.Criar(moto);
        return CreatedAtAction(nameof(ObterPorId), new { id = criada.Id }, criada);
    }

    /// <summary>
    /// Atualiza os dados de uma moto.
    /// </summary>
    [HttpPut]
    public IActionResult Atualizar(Moto moto)
    {
        var atualizado = _service.Atualizar(moto);
        if (!atualizado) return NotFound();

        return NoContent();
    }

    /// <summary>
    /// Remove uma moto pelo ID.
    /// </summary>
    [HttpDelete("{id}")]
    public IActionResult Remover(string id)
    {
        var removido = _service.Remover(id);
        if (!removido) return NotFound();

        return NoContent();
    }
}
