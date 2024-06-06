using GestionCandidatosApi.ConexionDB;
using GestionCandidatosApi.Modelos;
using Microsoft.EntityFrameworkCore;

namespace GestionCandidatosApi.Services
{
    public interface IUsuariosService{
        Task<List<Usuarios>> GetAll(Filtros filtro);
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
                List<Usuarios> Tarjetas = new List<Usuarios>();

                switch (filtro.FiltroPrimario) {
                    case "TODOS":
                        Tarjetas = await dbContext.Usuarios.ToListAsync();
                        break;
                     case "TODOSA": //usuarios activos
                        Tarjetas = await dbContext.Usuarios.Where(m => m.estado == "A").ToListAsync();
                        break;
                    default:
                        Tarjetas = await dbContext.Usuarios.ToListAsync();
                        break;
                }
                Tarjetas = await dbContext.Usuarios.ToListAsync();

                return Tarjetas;
            }
            catch (Exception e) {
                throw e;
            }
        }

        #endregion
    }
}
