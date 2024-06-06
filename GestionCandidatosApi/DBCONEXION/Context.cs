using GestionCandidatosApi.Modelos;
using Microsoft.EntityFrameworkCore;

namespace GestionCandidatosApi.ConexionDB
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }


        public DbSet<Usuarios> Usuarios { get; set; }
       
        protected override void OnModelCreating(ModelBuilder dbBuilder) 
        {
            dbBuilder.Entity<Usuarios>().ToTable("USUARIOS");
            dbBuilder.Entity<Usuarios>().HasKey(d => new { d.id });
           
        }
    }
}