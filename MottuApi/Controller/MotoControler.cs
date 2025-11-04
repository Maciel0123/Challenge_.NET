using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MottuBusiness;
using MottuModel;
using MottuApi.ML;

namespace MottuApi.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize]
[Produces("application/json")]
public class MotoController : ControllerBase
{
    private readonly IMottuService _service;

    public MotoController(IMottuService service)
    {
        _service = service;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<Moto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<List<Moto>> ListarTodos()
        => Ok(_service.ListarTodos());

    [HttpGet("paginado")]
    [ProducesResponseType(typeof(List<Moto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<List<Moto>> ListarPaginado(int page = 1, int pageSize = 10)
        => Ok(_service.ListarPaginado(page, pageSize));

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<object> ObterPorId(string id)
    {
        var moto = _service.ObterPorId(id);
        if (moto == null) return NotFound();

        var self = Url.Action(nameof(ObterPorId), new { id, version = "1" });
        var update = Url.Action(nameof(Atualizar), new { version = "1" });
        var delete = Url.Action(nameof(Remover), new { id, version = "1" });

        return Ok(new
        {
            data = moto,
            links = new[]
            {
                new { rel = "self",   href = self,   method = "GET" },
                new { rel = "update", href = update, method = "PUT" },
                new { rel = "delete", href = delete, method = "DELETE" }
            }
        });
    }

    [HttpPost]
    [ProducesResponseType(typeof(Moto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<Moto> Criar(Moto moto)
    {
        var criada = _service.Criar(moto);
        return CreatedAtAction(nameof(ObterPorId), new { id = criada.Id, version="1" }, criada);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult Atualizar(Moto moto)
    {
        var atualizado = _service.Atualizar(moto);
        if (!atualizado) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult Remover(string id)
    {
        var removido = _service.Remover(id);
        if (!removido) return NotFound();
        return NoContent();
    }

    public record MotoPredictRequest(string Modelo);

    [HttpPost("predict")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<object> Predict([FromBody] MotoPredictRequest req, [FromServices] IMotoRiskModel model)
    {
        if (string.IsNullOrWhiteSpace(req.Modelo))
            return BadRequest(new { erro = "Informe o campo 'Modelo'." });

        var pred = model.Predict(new MotoRiskData { Modelo = req.Modelo });

        var categoria = pred.Score switch
        {
            < 0.33f => "baixo",
            < 0.66f => "médio",
            _ => "alto"
        };

        return Ok(new
        {
            modelo = req.Modelo,
            risco = Math.Round(pred.Score, 3),
            categoria
        });
    }
}
