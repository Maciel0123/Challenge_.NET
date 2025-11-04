using MottuModel;
using Swashbuckle.AspNetCore.Filters;

namespace MottuApi.Examples
{
    public class MotoCreateExample : IExamplesProvider<Moto>
    {
        public Moto GetExamples()
        {
            return new Moto
            {
                Modelo = "Honda CG 160",
                Placa = "ABC1D23",
                ZonaId = 2
            };
        }
    }
}
