namespace F1API.Models
{
    public class Driver
    {
        public int DriverId { get; set; }
        public int Number { get; set; }
        public string Code { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public DateTime Dob { get; set; }
        public string Nationality { get; set; }
    }
}
