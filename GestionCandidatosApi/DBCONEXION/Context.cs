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
            dbBuilder.Entity<Usuarios>().Property(s => s.id).ValueGeneratedOnAdd();
            #endregion

            #region vacantes
            dbBuilder.Entity<Vacantes>().ToTable("VACANTES");
            dbBuilder.Entity<Vacantes>().HasKey(d => new { d.codigo_vacante });
            #endregion

            #region puestos
            dbBuilder.Entity<Puestos>().ToTable("PUESTOS");
            dbBuilder.Entity<Puestos>().HasKey(d => new { d.codigo_puesto });
            #endregion

            #region candidatos
            dbBuilder.Entity<Candidatos>().ToTable("CANDIDATOS");
            dbBuilder.Entity<Candidatos>().HasKey(d => new { d.codigo_candidato });
            #endregion

            #region entrevistas
            dbBuilder.Entity<Entrevistas>().ToTable("ENTREVISTAS");
            dbBuilder.Entity<Entrevistas>().HasKey(d => new { d.codigo_entrevista });
            #endregion

            #region permiso
            dbBuilder.Entity<Permiso>().ToTable("PERMISO");
            dbBuilder.Entity<Permiso>().HasKey(d => new { d.codigo_permiso });
            #endregion

            #region roles
            dbBuilder.Entity<Roles>().ToTable("ROLES");
            dbBuilder.Entity<Roles>().HasKey(d => new { d.codigo_rol });
            #endregion

            #region roles_permisos
            dbBuilder.Entity<Roles_Permisos>().ToTable("ROLES_PERMISOS");
            dbBuilder.Entity<Roles_Permisos>().HasKey(d => new { d.codigo_rol });
            #endregion

            #region usuarios_roles
            dbBuilder.Entity<Usuarios_Roles>().ToTable("USUARIOS_ROLES");
            dbBuilder.Entity<Usuarios_Roles>().HasKey(d => new { d.codigo_usuario });
            #endregion

            #region menus
            dbBuilder.Entity<Menus>().ToTable("MENUS");
            dbBuilder.Entity<Menus>().HasKey(d => new { d.id });
            #endregion
        }



    }
}