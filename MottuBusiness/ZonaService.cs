// ZonaService.cs (completo com paginação)
using MottuModel;
using MottuData;
using Microsoft.EntityFrameworkCore;

namespace MottuBusiness
{
    public class ZonaService : IZonaService
    {
        private readonly ApplicationDbContext _context;

        public ZonaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Zona> ListarTodos() =>
            _context.Zonas
                .Include(z => z.Patio)
                .Include(z => z.Motos)
                .ToList();

        public List<Zona> ListarPaginado(int page, int pageSize) =>
            _context.Zonas
                .Include(z => z.Patio)
                .Include(z => z.Motos)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

        public List<Zona> ListarPorPatio(Guid patioId) =>
            _context.Zonas
                .Where(z => z.PatioId == patioId)
                .ToList();

        public Zona ObterPorId(int id) =>
            _context.Zonas
                .Include(z => z.Motos)
                .Include(z => z.Patio)
                .FirstOrDefault(z => z.Id == id)!;

        public Zona Criar(Zona zona)
        {
            _context.Zonas.Add(zona);
            _context.SaveChanges();
            return zona;
        }

        public bool Atualizar(Zona zona)
        {
            var existente = _context.Zonas.Find(zona.Id);
            if (existente == null) return false;

            existente.Nome = zona.Nome;
            existente.PatioId = zona.PatioId;

            _context.SaveChanges();
            return true;
        }

        public bool Remover(int id)
        {
            var zona = _context.Zonas.Find(id);
            if (zona == null) return false;

            _context.Zonas.Remove(zona);
            _context.SaveChanges();
            return true;
        }
    }
}
