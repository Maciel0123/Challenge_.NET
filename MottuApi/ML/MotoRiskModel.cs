using Microsoft.ML;

namespace MottuApi.ML;

public interface IMotoRiskModel
{
    MotoRiskPrediction Predict(MotoRiskData input);
}

public class MotoRiskModel : IMotoRiskModel
{
    private readonly MLContext _ml;
    private readonly PredictionEngine<MotoRiskData, MotoRiskPrediction> _engine;

    public MotoRiskModel()
    {
        _ml = new MLContext(seed: 42);

        // Dataset mínimo de treino (exemplificativo)
        // A ideia: alguns modelos têm risco maior/menor
        var samples = new[]
        {
            new MotoRiskData { Modelo = "Honda CG 160" },
            new MotoRiskData { Modelo = "Honda CG 160" },
            new MotoRiskData { Modelo = "Honda Biz" },
            new MotoRiskData { Modelo = "Honda Biz" },
            new MotoRiskData { Modelo = "Yamaha Factor" },
            new MotoRiskData { Modelo = "Yamaha Fazer" },
            new MotoRiskData { Modelo = "Yamaha Fazer" },
            new MotoRiskData { Modelo = "Yamaha Fazer" },
            new MotoRiskData { Modelo = "Honda Start" },
            new MotoRiskData { Modelo = "Honda Start" },
        };

        // Para regressão precisamos de "Label" numérica.
        // Aqui criamos um label sintético por modelo (apenas para a sprint).
        var labeled = samples.Select(s => new
        {
            s.Modelo,
            Label = s.Modelo switch
            {
                "Honda Biz" => 0.25f,
                "Honda CG 160" => 0.45f,
                "Honda Start" => 0.60f,
                "Yamaha Factor" => 0.55f,
                "Yamaha Fazer" => 0.75f,
                _ => 0.50f
            }
        });

        var data = _ml.Data.LoadFromEnumerable(labeled);

        var pipeline =
            _ml.Transforms.Text.FeaturizeText("ModeloFeats", "Modelo") // transforma string em vetores
              .Append(_ml.Transforms.CopyColumns("Features", "ModeloFeats"))
              .Append(_ml.Regression.Trainers.Sdca(labelColumnName: "Label", featureColumnName: "Features"));

        var model = pipeline.Fit(data);

        _engine = _ml.Model.CreatePredictionEngine<MotoRiskData, MotoRiskPrediction>(model);
    }

    public MotoRiskPrediction Predict(MotoRiskData input)
    {
        var raw = _engine.Predict(input).Score;

        // clamp 0..1 por segurança didática
        var score = Math.Clamp(raw, 0f, 1f);
        return new MotoRiskPrediction { Score = score };
    }
}
