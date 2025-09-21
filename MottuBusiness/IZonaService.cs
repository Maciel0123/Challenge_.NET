using MottuModel;

namespace MottuBusiness
{
    public interface IZonaService
    {
        List<Zona> ListarTodos();
        List<Zona> ListarPaginado(int page, int pageSize);
        List<Zona> ListarPorPatio(Guid patioId);
        Zona ObterPorId(int id);
        Zona Criar(Zona zona);
        bool Atualizar(Zona zona);
        bool Remover(int id);
    }
}