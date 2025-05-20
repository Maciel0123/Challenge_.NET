using System.Collections.Generic;
using MottuModel;

namespace MottuModel
{
    public class Zona
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public Guid? PatioId { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public Patio? Patio { get; set; }
        public ICollection<Moto> Motos { get; set; } = new List<Moto>();
    }
}
