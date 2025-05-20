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
    }
}
