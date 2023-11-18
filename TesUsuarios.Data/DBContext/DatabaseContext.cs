using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesUsuarios.Data.Entities;

namespace TesUsuarios.Data.DBContext
{
    public class DatabaseContext : DbContext
    {
        public DbSet<UsuariosDataModel> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "ApplicantDB");
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<UsuariosDataModel>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<UsuariosDataModel>()
                .Property(u => u.Name)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<UsuariosDataModel>()
                .Property(u => u.Email)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<UsuariosDataModel>()
                .Property(u => u.Age)
                .IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
