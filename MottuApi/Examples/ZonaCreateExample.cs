using MottuModel;
using Swashbuckle.AspNetCore.Filters;

namespace MottuApi.Examples
{
    public class ZonaCreateExample : IExamplesProvider<Zona>
    {
        public Zona GetExamples()
        {
            return new Zona
            {
                Nome = "ZONA CENTRAL",
                PatioId = Guid.Parse("2c6ab862-9a33-4d5f-b52b-44fa018d7852")
            };
        }
    }
}
