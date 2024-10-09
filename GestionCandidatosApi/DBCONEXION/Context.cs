using GestionCandidatosApi.Modelos;
using Microsoft.EntityFrameworkCore;

namespace GestionCandidatosApi.ConexionDB
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }


        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Vacantes> Vacantes { get; set; }
        public DbSet<Puestos> Puestos { get; set; }
        public DbSet<Candidatos> Candidatos { get; set; }
        public DbSet<Entrevistas> Entrevistas { get; set; }
        public DbSet<Permiso> Permisos { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Roles_Permisos> Roles_Permisos { get; set; }
        public DbSet<Usuarios_Roles> Usuarios_Roles { get; set; }
        


        protected override void OnModelCreating(ModelBuilder dbBuilder) 
        {
            #region usuarios
            dbBuilder.Entity<Usuarios>().ToTable("USUARIOS");
            dbBuilder.Entity<Usuarios>().HasKey(d => new { d.id });
            #endregion

            #region vacantes
            dbBuilder.Entity<Vacantes>().ToTable("VACANTES");
            dbBuilder.Entity<Vacantes>().HasKey(d => new { d.id });
            #endregion

            #region puestos
            dbBuilder.Entity<Puestos>().ToTable("PUESTOS");
            dbBuilder.Entity<Puestos>().HasKey(d => new { d.id });
            #endregion

            #region candidatos
            dbBuilder.Entity<Puestos>().ToTable("CANDIDATOS");
            dbBuilder.Entity<Puestos>().HasKey(d => new { d.id });
            #endregion

            #region entrevistas
            dbBuilder.Entity<Puestos>().ToTable("ENTREVISTAS");
            dbBuilder.Entity<Puestos>().HasKey(d => new { d.id });
            #endregion

            #region permiso
            dbBuilder.Entity<Puestos>().ToTable("PERMISO");
            dbBuilder.Entity<Puestos>().HasKey(d => new { d.id });
            #endregion

            #region roles
            dbBuilder.Entity<Puestos>().ToTable("ROLES");
            dbBuilder.Entity<Puestos>().HasKey(d => new { d.id });
            #endregion

            #region roles_permisos
            dbBuilder.Entity<Puestos>().ToTable("ROLES_PERMISOS");
            dbBuilder.Entity<Puestos>().HasKey(d => new { d.id });
            #endregion

            #region usuarios_roles
            dbBuilder.Entity<Puestos>().ToTable("USUARIOS_ROLES");
            dbBuilder.Entity<Puestos>().HasKey(d => new { d.id });
            #endregion
        }



    }
}