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

    /// <summary>
    /// Lista todos os pátios cadastrados, incluindo suas zonas.
    /// </summary>
    /// <returns>Lista de pátios</returns>
    [HttpGet]
    public ActionResult<List<Patio>> ListarTodos()
    {
        return Ok(_service.ListarTodos());
    }

    /// <summary>
    /// Lista os pátios com paginação.
    /// </summary>
    /// <param name="page">Número da página (inicia em 1)</param>
    /// <param name="pageSize">Quantidade de itens por página</param>
    /// <returns>Lista paginada de pátios</returns>
    [HttpGet("paginado")]
    public ActionResult<List<Patio>> ListarPaginado(int page = 1, int pageSize = 10)
    {
        return Ok(_service.ListarPaginado(page, pageSize));
    }

    /// <summary>
    /// Busca um pátio por ID.
    /// </summary>
    /// <param name="id">ID do pátio</param>
    /// <returns>Objeto pátio, se encontrado</returns>
    [HttpGet("{id}")]
    public ActionResult<Patio> ObterPorId(Guid id)
    {
        var patio = _service.ObterPorId(id);
        if (patio == null)
            return NotFound();

        return Ok(patio);
    }

    /// <summary>
    /// Cria um novo pátio.
    /// </summary>
    /// <param name="patio">Dados do novo pátio</param>
    /// <returns>Pátio criado</returns>
    [HttpPost]
    public ActionResult<Patio> Criar(Patio patio)
    {
        var criado = _service.Criar(patio);
        return CreatedAtAction(nameof(ObterPorId), new { id = criado.Id }, criado);
    }

    /// <summary>
    /// Atualiza um pátio existente.
    /// </summary>
    /// <param name="patio">Objeto com os dados atualizados</param>
    /// <returns>NoContent se atualizado com sucesso</returns>
    [HttpPut]
    public IActionResult Atualizar(Patio patio)
    {
        var atualizado = _service.Atualizar(patio);
        if (!atualizado)
            return NotFound();

        return NoContent();
    }

    /// <summary>
    /// Remove um pátio pelo ID.
    /// </summary>
    /// <param name="id">ID do pátio</param>
    /// <returns>NoContent se removido com sucesso</returns>
    [HttpDelete("{id}")]
    public IActionResult Remover(Guid id)
    {
        var removido = _service.Remover(id);
        if (!removido)
            return NotFound();

        return NoContent();
    }
}
