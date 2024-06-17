using GestionCandidatosApi.ConexionDB;
using GestionCandidatosApi.Modelos;
using Microsoft.EntityFrameworkCore;

namespace GestionCandidatosApi.Services
{
    public interface IUsuariosService{
        Task<List<Usuarios>> GetAll(Filtros filtro);
        Task<string> InsertUsuarios(Usuarios modelo);
        Task<int> UpdateUsuarios(Usuarios modelo);
        Task<Usuarios> GetUsuario(Filtros filtro);
    }
    public class UsuariosService : IUsuariosService
    {

        private readonly Context dbContext;

        public UsuariosService(Context _dbContext) { 
            dbContext = _dbContext;
        }

        #region SELECT
        public async Task<List<Usuarios>> GetAll(Filtros filtro)
        {
            try
            {
                List<Usuarios> Usuarios = new List<Usuarios>();

                switch (filtro.FiltroPrimario) {
                    case "TODOS":
                        Usuarios = await dbContext.Usuarios.ToListAsync();
                        break;
                     case "TODOSA": //usuarios activos
                        Usuarios = await dbContext.Usuarios.Where(m => m.estado == "A").ToListAsync();
                        break;
                     case "NOMBRE": //USUARIOS POR NOMBRE
                        Usuarios = await dbContext.Usuarios.Where(m => m.nombre.Contains(filtro.FiltroSecundario)).ToListAsync();
                        break;
                    default:
                        Usuarios = await dbContext.Usuarios.ToListAsync();
                        break;
                }
                Usuarios = await dbContext.Usuarios.ToListAsync();

                return Usuarios;
            }
            catch (Exception e) {
                throw e;
            }
        }
        #endregion

        #region SELECT USER
        public async Task<Usuarios> GetUsuario(Filtros filtro)
        {
            try
            {
                var usuario = await dbContext.Usuarios.Where(m => m.username == filtro.FiltroPrimario && m.password == filtro.FiltroSecundario).FirstOrDefaultAsync();

                if (VariosService.VerifyPassword(filtro.FiltroSecundario, usuario.password))
                {
                    return usuario;
                }
                else {
                    return null;
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region INSERT 
        public async Task<string> InsertUsuarios(Usuarios modelo)
        {
            try
            {
                modelo.username = modelo.username != null ? modelo.username.ToUpper() : "no data";
                modelo.estado = modelo.estado != null ? modelo.estado : "A";
                modelo.password = VariosService.EncryptPassword(modelo.password);

                dbContext.Usuarios.Add(modelo);
                dbContext.SaveChanges();
                return "Exito";
            }
            catch (Exception e)
            {
                throw new Exception("Error al insertar los usuarios al sistema");
            }
        }

        #endregion

        #region UPDATE 
        public async Task<int> UpdateUsuarios(Usuarios modelo)
        {
            try
            {
                var ejecuta = 0; //verifica si existe
                var usuario = await dbContext.Usuarios.Where(m => m.id == modelo.id).FirstOrDefaultAsync();
                if (usuario != null)
                {
                    if (VariosService.IsSamePassword(modelo.password, usuario.password))
                    {
                        
                    }
                    else {
                        usuario.email = modelo.email;
                        usuario.password = modelo.password;
                        usuario.estado = modelo.estado;
                        dbContext.Usuarios.Add(usuario);
                        ejecuta = 0;
                    }
                }
                else {
                    ejecuta = 1;
                }

                return ejecuta;
            }
            catch (Exception e)
            {
                throw new Exception("Error al insertar los usuarios al sistema");
            }
        }

        #endregion
    }
}
