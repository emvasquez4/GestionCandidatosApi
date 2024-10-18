using GestionCandidatosApi.ConexionDB;
using GestionCandidatosApi.Modelos;
using Microsoft.EntityFrameworkCore;

namespace GestionCandidatosApi.Services
{
    public interface IRolesService
    {
        Task<List<Roles>> GetAll(Filtros filtro);
    }
    public class RolesService : IRolesService
    {
        private readonly Context dbContext;

        public RolesService(Context _dbContext)
        {
            dbContext = _dbContext;
        }


        #region SELECT
        public async Task<List<Roles>> GetAll(Filtros filtro)
        {
            try
            {
                List<Roles> Roles = new List<Roles>();

                switch (filtro.FiltroPrimario)
                {
                    case "TODOS":
                        Roles = await dbContext.Roles.ToListAsync();
                        break;
                    case "TODOSA": //usuarios activos
                        Roles = await dbContext.Roles.Where(m => m.estado == "A").ToListAsync();
                        break;
                    case "NOMBRE": //USUARIOS POR NOMBRE
                        Roles = await dbContext.Roles.Where(m => m.descripcion.Contains(filtro.FiltroSecundario)).ToListAsync();
                        break;
                    case "CODIGO":
                        Roles = await dbContext.Roles.Where(m => m.codigo_rol == filtro.FiltroSecundario).ToListAsync();
                        break;
                    default:
                        Roles = await dbContext.Roles.ToListAsync();
                        break;
                }
                

                return Roles;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion(Context _dbContext)
        

    }
}
