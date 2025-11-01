using MottuModel;
using MottuData;
using Microsoft.EntityFrameworkCore;

namespace MottuBusiness
{
    public class PatioService : IPatioService
    {
        private readonly ApplicationDbContext _context;

        public PatioService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Patio> ListarTodos() => _context.Patios.Include(p => p.Zonas).ToList();

        public List<Patio> ListarPaginado(int page, int pageSize)
        {
            return _context.Patios
                .AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public Patio? ObterPorId(Guid id) => _context.Patios.Include(p => p.Zonas).FirstOrDefault(p => p.Id == id);

        public Patio Criar(Patio patio)
        {
            _context.Patios.Add(patio);
            _context.SaveChanges();
            return patio;
        }

        public bool Atualizar(Patio patio)
        {
            var existente = _context.Patios.Find(patio.Id);
            if (existente == null) return false;

            existente.Nome = patio.Nome;

            _context.SaveChanges();
            return true;
        }

        public bool Remover(Guid id)
        {
            var patio = _context.Patios.Find(id);
            if (patio == null) return false;

            _context.Patios.Remove(patio);
            _context.SaveChanges();
            return true;
        }
    }
}
