using MottuModel;

namespace MottuBusiness
{
public interface IPatioService
    {
        List<MottuModel.Patio> ListarTodos();
        Patio? ObterPorId(Guid id);
        Patio Criar(Patio patio);
    }
}