using GestionCandidatosApi.ConexionDB;
using GestionCandidatosApi.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionCandidatosApi.Services
{

    public interface IRolesPermisosService {
        Task<List<Roles_Permisos>> GetAll(Filtros filtro);
        Task<string> InsertRP(Roles_Permisos modelo);
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

        #region INSERT 
        public async Task<string> InsertRP(Roles_Permisos modelo)
        {

            try
            {
                
                await _dbContext.Roles_Permisos.AddAsync(modelo);
                await _dbContext.SaveChangesAsync();
                //transaction.Commit();
                return "Exito";
            }
            catch (Exception e)
            {
                //transaction.Rollback();
                throw new Exception("Error al insertar permisos al rol");
            }
        }
        #endregion
    }
}
