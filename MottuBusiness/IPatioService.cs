using MottuModel;


namespace MottuBusiness
{
public interface IPatioService
{
List<Patio> ListarTodos();
List<Patio> ListarPaginado(int page, int pageSize);
Patio? ObterPorId(Guid id);
Patio Criar(Patio patio);
bool Atualizar(Patio patio);
bool Remover(Guid id);
}
}