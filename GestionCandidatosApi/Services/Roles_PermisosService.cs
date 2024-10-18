using GestionCandidatosApi.ConexionDB;
using GestionCandidatosApi.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionCandidatosApi.Services
{

    public interface IRolesPermisosService {
        Task<List<Roles_Permisos>> GetAll(Filtros filtro);
    }
    public class Roles_PermisosService : IRolesPermisosService
    {
        private readonly Context _dbContext;

        public Roles_PermisosService(Context dbContext)
        {
            _dbContext = dbContext;
        }

        #region SELECT
        public async Task<List<Roles_Permisos>> GetAll(Filtros filtro)
        {
            try
            {
                List<Roles_Permisos> RolesP = new List<Roles_Permisos>();

                switch (filtro.FiltroPrimario)
                {
                    case "TODOS":
                        RolesP = await _dbContext.Roles_Permisos.ToListAsync();
                        break;
                    case "CODPERMISO": //codpermiso
                        RolesP = await _dbContext.Roles_Permisos.Where(m => m.codigo_permiso == filtro.FiltroSecundario).ToListAsync();
                        break;
                    case "CODIGOROL":
                        RolesP = await _dbContext.Roles_Permisos.Where(m => m.codigo_rol == filtro.FiltroSecundario).ToListAsync();
                        break;
                    default:
                        RolesP = await _dbContext.Roles_Permisos.ToListAsync();
                        break;
                }


                return RolesP;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion
    }
}
