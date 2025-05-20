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

        public List<MottuModel.Patio> ListarTodos() => _context.Patios.Include(p => p.Zonas).ToList();

        public Patio? ObterPorId(Guid id) => _context.Patios.Include(p => p.Zonas).FirstOrDefault(p => p.Id == id);

        public Patio Criar(Patio patio)
        {
            _context.Patios.Add(patio);
            _context.SaveChanges();
            return patio;
        }
    }
}