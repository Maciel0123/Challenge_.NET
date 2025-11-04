namespace MottuApi.ML;

public class MotoRiskData
{
    public string Modelo { get; set; } = "";
}

public class MotoRiskPrediction
{
    public float Score { get; set; } // 0..1
}
