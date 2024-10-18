using GestionCandidatosApi.ConexionDB;
using GestionCandidatosApi.Modelos;
using Microsoft.EntityFrameworkCore;

namespace GestionCandidatosApi.Services
{
    public interface IPermisosService {
        Task<List<Permiso>> GetAll(Filtros filtro);
    }
    public class PermisoService : IPermisosService
    {
        private readonly Context dbContext;

        public PermisoService(Context _dbContext)
        {
            dbContext = _dbContext;
        }


        #region SELECT
        public async Task<List<Permiso>> GetAll(Filtros filtro)
        {
            try
            {
                List<Permiso> Permiso = new List<Permiso>();

                switch (filtro.FiltroPrimario)
                {
                    case "TODOS":
                        Permiso = await dbContext.Permisos.ToListAsync();
                        break;
                    case "TODOSA": //usuarios activos
                        Permiso = await dbContext.Permisos.Where(m => m.estado == "A").ToListAsync();
                        break;
                    case "NOMBRE": //USUARIOS POR NOMBRE
                        Permiso = await dbContext.Permisos.Where(m => m.descripcion.Contains(filtro.FiltroSecundario)).ToListAsync();
                        break;
                    case "CODPERMISO":
                        Permiso = await dbContext.Permisos.Where(m => m.codigo_permiso == filtro.FiltroSecundario).ToListAsync();
                        break;
                    default:
                        Permiso = await dbContext.Permisos.ToListAsync();
                        break;
                }
                Permiso = await dbContext.Permisos.ToListAsync();

                return Permiso;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion
    }

}
