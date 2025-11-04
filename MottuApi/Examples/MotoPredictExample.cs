using Swashbuckle.AspNetCore.Filters;
using MottuApi.Controllers; 

namespace MottuApi.Examples
{
    public class MotoPredictExample : IExamplesProvider<MotoController.MotoPredictRequest>
    {
        public MotoController.MotoPredictRequest GetExamples() =>
            new MotoController.MotoPredictRequest("Honda CB 300");
    }
}
