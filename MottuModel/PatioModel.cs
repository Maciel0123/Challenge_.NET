namespace MottuModel
{
    public class Patio
    {
        public  Guid? Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        [System.Text.Json.Serialization.JsonIgnore]
        public ICollection<Zona> Zonas { get; set; } = new List<Zona>();
    }
}
