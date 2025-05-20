namespace MottuModel;

public class Moto
{
    public Guid? Id { get; set; }
    public required string Modelo { get; set; }
    public required string Placa { get; set; }
    public int ZonaId { get; set; }
    [System.Text.Json.Serialization.JsonIgnore]
    public Zona? Zona { get; set; }

}