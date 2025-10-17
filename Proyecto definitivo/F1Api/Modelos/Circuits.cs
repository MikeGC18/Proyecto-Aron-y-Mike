namespace F1Api.Models
{
    public class Circuit
    {
        public int CircuitId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Country { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
    }
}
