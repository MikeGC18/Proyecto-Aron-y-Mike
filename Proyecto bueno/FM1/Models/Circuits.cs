namespace F1API.Models
{
    public class Circuit
    {
        public int CircuitId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Country { get; set; }
        public decimal Lat { get; set; }
        public decimal Lng { get; set; }
    }
}