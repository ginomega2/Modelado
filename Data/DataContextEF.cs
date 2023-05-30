using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Modelado.Models;

namespace Modelado.Data
{


    public class DataContextEF : DbContext
    {

        // private string _conectionString;
        private IConfiguration _config;

        public DataContextEF(IConfiguration config)
        {
            _config = config;

            // _conectionString = config.GetConnectionString("DefaultConnection");

        }
        public DbSet<Computer>? Computer { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer(_config.GetConnectionString("DefaultConnection"),
                options => options.EnableRetryOnFailure());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("TutorialAppSchema");
            modelBuilder.Entity<Computer>()
            .HasKey(c => c.ComputerId);
            // .HasNoKey();
            // .ToTable("Computer","TutorialAppSchema");
            // .ToTable("Tabla","Nombre de Schema");
        }
    }

}