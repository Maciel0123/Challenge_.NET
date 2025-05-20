using MottuModel;

namespace MottuBusiness
{
    public interface IMottuService
    {
        List<MottuModel.Moto> ListarTodos();
        MottuModel.Moto? ObterPorId(string id);
        MottuModel.Moto Criar(MottuModel.Moto motos);
        bool Atualizar(MottuModel.Moto motos);
        bool Remover(string id);
    }
}