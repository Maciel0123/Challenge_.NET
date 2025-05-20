using MottuModel;
using MottuData;
using Microsoft.EntityFrameworkCore;

namespace MottuBusiness;

public class MottuService : IMottuService
{
    private readonly ApplicationDbContext _context;

    public MottuService(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<MottuModel.Moto> ListarTodos() =>
        _context.Motos.ToList(); // Retorna a lista de todas as Mottu

    public MottuModel.Moto? ObterPorId(string id)
    {
        if (!Guid.TryParse(id, out var parsedId)) // Converte string para Guid
            return null; // Se a conversão falhar, retorna null

        return _context.Motos.FirstOrDefault(c => c.Id == parsedId); // Comparando Guid com Guid
    }

    public MottuModel.Moto Criar(MottuModel.Moto motos)
    {
        _context.Motos.Add(motos);
        _context.SaveChanges();
        return motos;
    }

    public bool Atualizar(MottuModel.Moto motos)
    {
        var existente = _context.Motos.Find(motos.Id);
        if (existente == null) return false;

        existente.Modelo = motos.Modelo;
        existente.Placa = motos.Placa;

        _context.SaveChanges();
        return true;
    }

    public bool Remover(string id)
    {
        if (!Guid.TryParse(id, out var parsedId)) // Verifica se o id é um Guid válido
            return false;

        var moto = _context.Motos.Find(parsedId); // Busca pela chave Guid
        if (moto == null) return false;

        _context.Motos.Remove(moto);
        _context.SaveChanges();
        return true;
    }
}