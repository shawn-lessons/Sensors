using System;
using Microsoft.EntityFrameworkCore;

namespace Sensors
{
    public class SensorContext : DbContext
    {
        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<Reading> Readings { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options
               .UseSqlServer("Server=sql1.uict.nz;Database=Shawn;Trusted_Connection=True;TrustServerCertificate=True;");
        }

    }
}