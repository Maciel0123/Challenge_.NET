using Microsoft.AspNetCore.Mvc;
using MottuBusiness;
using MottuModel;
using Model = MottuModel.Moto;

namespace MottuApi.Controller;

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

    [HttpGet("{id}")]
    public IActionResult Get(Guid id)
    {
        var patio = _service.ObterPorId(id);
        return patio == null ? NotFound() : Ok(patio);
    }

    [HttpPost]
    public IActionResult Post([FromBody] Patio patio)
    {
        var criado = _service.Criar(patio);
        return CreatedAtAction(nameof(Get), new { id = criado.Id }, criado);
    }
}
