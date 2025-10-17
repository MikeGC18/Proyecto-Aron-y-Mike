using MySql.Data.MySqlClient;
using F1API.Models;

namespace F1API.Data
{
    public class F1Repository
    {
        private string connectionString = "server=localhost;database=f1database;user=root;password=;";

        // CIRCUITS
        public List<Circuit> GetCircuits()
        {
            var circuits = new List<Circuit>();
            using var con = new MySqlConnection(connectionString);
            con.Open();
            var cmd = new MySqlCommand("SELECT * FROM Circuits", con);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                circuits.Add(new Circuit
                {
                    CircuitId = reader.GetInt32("CircuitId"),
                    Name = reader.GetString("Name"),
                    Location = reader.GetString("Location"),
                    Country = reader.GetString("Country"),
                    Lat = reader.GetDecimal("Lat"),
                    Lng = reader.GetDecimal("Lng")
                });
            }
            return circuits;
        }

        // DRIVERS
        public List<Driver> GetDrivers()
        {
            var drivers = new List<Driver>();
            using var con = new MySqlConnection(connectionString);
            con.Open();
            var cmd = new MySqlCommand("SELECT * FROM Drivers", con);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                drivers.Add(new Driver
                {
                    DriverId = reader.GetInt32("DriverId"),
                    Number = reader.GetInt32("Number"),
                    Code = reader.GetString("Code"),
                    Forename = reader.GetString("Forename"),
                    Surname = reader.GetString("Surname"),
                    Dob = reader.GetDateTime("Dob"),
                    Nationality = reader.GetString("Nationality")
                });
            }
            return drivers;
        }

        // CONSTRUCTORS
        public List<Constructor> GetConstructors()
        {
            var constructors = new List<Constructor>();
            using var con = new MySqlConnection(connectionString);
            con.Open();
            var cmd = new MySqlCommand("SELECT * FROM Constructors", con);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                constructors.Add(new Constructor
                {
                    ConstructorId = reader.GetInt32("ConstructorId"),
                    Name = reader.GetString("Name"),
                    Nationality = reader.GetString("Nationality")
                });
            }
            return constructors;
        }
    }
}
