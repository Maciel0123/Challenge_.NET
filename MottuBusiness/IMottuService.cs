using MottuModel;

namespace MottuBusiness
{
    public interface IMottuService
    {
        List<Moto> ListarTodos();
        List<Moto> ListarPaginado(int page, int pageSize);
        Moto? ObterPorId(string id);
        Moto Criar(Moto moto);
        bool Atualizar(Moto moto);
        bool Remover(string id);
    }
}
