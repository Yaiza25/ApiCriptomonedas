namespace ApiMonedas.Models
{
    public class Criptomonedas
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public decimal Ultimo { get; set; }
        public decimal Maximo { get; set; }
    }
}