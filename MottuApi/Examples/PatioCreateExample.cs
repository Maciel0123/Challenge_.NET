using MottuModel;
using Swashbuckle.AspNetCore.Filters;

namespace MottuApi.Examples
{
    public class PatioCreateExample : IExamplesProvider<Patio>
    {
        public Patio GetExamples()
        {
            return new Patio
            {
                Nome = "Pátio São Paulo - Mooca"
            };
        }
    }
}