using System;
using System.Data.SQLite;   // Assegura’t d’instal·lar el paquet: System.Data.SQLite (NuGet)
using System.IO;

class Program
{
    static void Main()
    {
        string dbFile = "f1.db";

        // Si el fitxer de BD ja existeix, l’esborrem per començar de zero
        if (File.Exists(dbFile))
        {
            File.Delete(dbFile);
            Console.WriteLine("Base de dades antiga eliminada.");
        }

        // Crear la nova base de dades SQLite
        SQLiteConnection.CreateFile(dbFile);
        Console.WriteLine("Base de dades creada correctament: " + dbFile);

        // Connexió a la BD
        using (var connection = new SQLiteConnection("Data Source=" + dbFile))
        {
            connection.Open();
            Console.WriteLine("Connexió establerta correctament.");

            string script = @"
            -- ============================
            -- ELIMINACIÓ DE TAULES (per reiniciar si cal)
            DROP TABLE IF EXISTS results;
            DROP TABLE IF EXISTS drivers;
            DROP TABLE IF EXISTS constructors;
            DROP TABLE IF EXISTS circuits;

            -- ============================
            -- TAULA: CIRCUITS
            CREATE TABLE circuits (
                circuitId      INTEGER PRIMARY KEY,
                name           TEXT NOT NULL,
                location       TEXT NOT NULL,
                country        TEXT NOT NULL,
                lat            REAL NOT NULL,
                lng            REAL NOT NULL
            );

            -- ============================
            -- TAULA: CONSTRUCTORS
            CREATE TABLE constructors (
                constructorId  INTEGER PRIMARY KEY,
                name           TEXT NOT NULL,
                nationality    TEXT NOT NULL
            );

            -- ============================
            -- TAULA: DRIVERS
            CREATE TABLE drivers (
                driverId       INTEGER PRIMARY KEY,
                number         INTEGER,
                code           TEXT,
                forename       TEXT NOT NULL,
                surname        TEXT NOT NULL,
                dob            TEXT NOT NULL,
                nationality    TEXT NOT NULL,
                constructorId  INTEGER,
                FOREIGN KEY (constructorId) REFERENCES constructors(constructorId)
            );

            -- ============================
            -- TAULA: RESULTS
            CREATE TABLE results (
                resultId       INTEGER PRIMARY KEY AUTOINCREMENT,
                driverId       INTEGER NOT NULL,
                circuitId      INTEGER NOT NULL,
                position       INTEGER,
                points         REAL DEFAULT 0,
                raceDate       TEXT NOT NULL,
                FOREIGN KEY (driverId) REFERENCES drivers(driverId),
                FOREIGN KEY (circuitId) REFERENCES circuits(circuitId)
            );

            -- ============================
            -- INSERCIÓ DE DADES DE PROVA
            -- CIRCUITS
            INSERT INTO circuits (circuitId, name, location, country, lat, lng) VALUES
            (1, 'Circuit de Barcelona-Catalunya', 'Montmeló', 'Spain', 41.57, 2.26),
            (2, 'Silverstone Circuit', 'Silverstone', 'United Kingdom', 52.07, -1.01),
            (3, 'Monza Circuit', 'Monza', 'Italy', 45.62, 9.28),
            (4, 'Suzuka International', 'Suzuka', 'Japan', 34.84, 136.54);

            -- CONSTRUCTORS
            INSERT INTO constructors (constructorId, name, nationality) VALUES
            (1, 'Mercedes AMG Petronas', 'German'),
            (2, 'Red Bull Racing', 'Austrian'),
            (3, 'Ferrari', 'Italian'),
            (4, 'McLaren', 'British');

            -- DRIVERS
            INSERT INTO drivers (driverId, number, code, forename, surname, dob, nationality, constructorId) VALUES
            (44, 44, 'HAM', 'Lewis', 'Hamilton', '1985-01-07', 'British', 1),
            (33, 33, 'VER', 'Max', 'Verstappen', '1997-09-30', 'Dutch', 2),
            (16, 16, 'LEC', 'Charles', 'Leclerc', '1997-10-16', 'Monégasque', 3),
            (4,  4, 'NOR', 'Lando', 'Norris', '1999-11-13', 'British', 4);

            -- RESULTS
            INSERT INTO results (driverId, circuitId, position, points, raceDate) VALUES
            (44, 1, 1, 25, '2024-05-12'),
            (33, 1, 2, 18, '2024-05-12'),
            (16, 2, 1, 25, '2024-06-05'),
            (44, 2, 3, 15, '2024-06-05'),
            (4,  3, 2, 18, '2024-07-01');
            ";

            // Executar l’script SQL
            using (var command = new SQLiteCommand(script, connection))
            {
                command.ExecuteNonQuery();
                Console.WriteLine("Taules creades i dades de prova inserides correctament!");
            }

            // Comprovació: comptar circuits
            using (var checkCmd = new SQLiteCommand("SELECT COUNT(*) FROM circuits;", connection))
            {
                long totalCircuits = (long)checkCmd.ExecuteScalar();
                Console.WriteLine("Nombre de circuits inserits: " + totalCircuits);
            }
        }

        Console.WriteLine("Procés finalitzat.");
    }
}
