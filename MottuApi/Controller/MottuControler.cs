using Microsoft.AspNetCore.Mvc;
using MottuBusiness;
using Model = MottuModel.Moto;

namespace MottuApi.Controller;

[ApiController]
[Route("api/[controller]")]
public class MottuController(IMottuService mottuService) : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        var motos = mottuService.ListarTodos();
        return motos.Count == 0 ? NoContent() : Ok(motos);
    }

    [HttpGet("paginado")]
    public IActionResult GetPaginado([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var motos = mottuService.ListarPaginado(page, pageSize);
        return motos.Count == 0 ? NoContent() : Ok(motos);
    }

    [HttpGet("{id}")]
    public IActionResult Get(string id)
    {
        var moto = mottuService.ObterPorId(id);
        if (moto == null) return NotFound();

        var response = new
        {
            moto.Id,
            moto.Modelo,
            moto.Placa,
            moto.ZonaId,
            links = new[]
            {
                new { rel = "self", href = Url.Action(nameof(Get), new { id = moto.Id }), method = "GET" },
                new { rel = "update", href = Url.Action(nameof(Put)), method = "PUT" },
                new { rel = "delete", href = Url.Action(nameof(Delete), new { id = moto.Id }), method = "DELETE" }
            }
        };

        return Ok(response);
    }

    [HttpPost]
    public IActionResult Post([FromBody] Model moto)
    {
        if (string.IsNullOrWhiteSpace(moto.Modelo))
            return BadRequest("Modelos é obrigatório.");
        var criado = mottuService.Criar(moto);
        return CreatedAtAction(nameof(Get), new { id = criado.Id }, criado);
    }

    [HttpPut]
    public IActionResult Put([FromBody] Model moto)
    {
        if (moto == null)
            return BadRequest("Dados inconsistentes.");
        return mottuService.Atualizar(moto) ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(string id)
    {
        return mottuService.Remover(id) ? NoContent() : NotFound();
    }
}