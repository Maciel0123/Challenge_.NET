using MottuModel;

namespace MottuBusiness
{
    public interface IZonaService
    {
        List<Zona> ListarTodos();
        List<Zona> ListarPorPatio(Guid patioId); 
        Zona ObterPorId(int id);
        Zona Criar(Zona zona);
    }
}
