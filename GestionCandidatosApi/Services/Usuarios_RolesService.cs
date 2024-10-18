using GestionCandidatosApi.ConexionDB;
using GestionCandidatosApi.Modelos;
using Microsoft.EntityFrameworkCore;

namespace GestionCandidatosApi.Services
{
    public interface IUsuariosRolesService {
        Task<List<Usuarios_Roles>> GetAll(Filtros filtro);
    }
    public class Usuarios_RolesService : IUsuariosRolesService
    {
        private readonly Context dbContext;

        public Usuarios_RolesService(Context _dbContext)
        {
            dbContext = _dbContext;
        }


        #region SELECT
        public async Task<List<Usuarios_Roles>> GetAll(Filtros filtro)
        {
            try
            {
                List<Usuarios_Roles> Roles = new List<Usuarios_Roles>();

                switch (filtro.FiltroPrimario)
                {
                    case "TODOS":
                        Roles = await dbContext.Usuarios_Roles.ToListAsync();
                        break;
                    case "CODUSUARIO": //USUARIOS
                        Roles = await dbContext.Usuarios_Roles.Where(m => m.codigo_usuario == Convert.ToInt32(filtro.FiltroSecundario)).ToListAsync();
                        break;
                    case "ROL":
                        Roles = await dbContext.Usuarios_Roles.Where(m => m.codigo_rol == filtro.FiltroSecundario).ToListAsync();
                        break;
                    default:
                        Roles = await dbContext.Usuarios_Roles.ToListAsync();
                        break;
                }


                return Roles;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion
    }
}
