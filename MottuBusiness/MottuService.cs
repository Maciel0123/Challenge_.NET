using MottuModel;
using MottuData;

namespace MottuBusiness;

public class MottuService : IMottuService
{
    private readonly ApplicationDbContext _context;

    public MottuService(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<Moto> ListarTodos() =>
        _context.Motos.ToList();

    public List<Moto> ListarPaginado(int page, int pageSize) =>
        _context.Motos
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

    public Moto? ObterPorId(string id)
    {
        if (!Guid.TryParse(id, out var parsedId))
            return null;

        return _context.Motos.FirstOrDefault(c => c.Id == parsedId);
    }

    public Moto Criar(Moto moto)
    {
        _context.Motos.Add(moto);
        _context.SaveChanges();
        return moto;
    }

    public bool Atualizar(Moto moto)
    {
        var existente = _context.Motos.Find(moto.Id);
        if (existente == null) return false;

        existente.Modelo = moto.Modelo;
        existente.Placa = moto.Placa;
        existente.ZonaId = moto.ZonaId;

        _context.SaveChanges();
        return true;
    }

    public bool Remover(string id)
    {
        if (!Guid.TryParse(id, out var parsedId))
            return false;

        var moto = _context.Motos.Find(parsedId);
        if (moto == null) return false;

        _context.Motos.Remove(moto);
        _context.SaveChanges();
        return true;
    }
}
