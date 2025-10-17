using Microsoft.EntityFrameworkCore;
using F1Api.Models;

namespace F1Api.Data
{
    public class Fm1Context : DbContext
    {
        public Fm1Context(DbContextOptions<Fm1Context> options) : base(options) { }

        public DbSet<Circuit> Circuits { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Constructor> Constructors { get; set; }
    }
}
