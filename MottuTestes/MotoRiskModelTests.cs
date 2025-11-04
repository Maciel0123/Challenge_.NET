using FluentAssertions;
using MottuApi.ML;

namespace MottuTestes;

public class MotoRiskModelTests
{
    [Fact]
    public void ML_Deve_Retornar_Score_Entre_0_e_1()
    {
        var model = new MotoRiskModel();

        var result = model.Predict(new MotoRiskData { Modelo = "Yamaha Fazer" });

        result.Score.Should().BeGreaterOrEqualTo(0f);
        result.Score.Should().BeLessOrOrEqualTo(1f);
    }
}
