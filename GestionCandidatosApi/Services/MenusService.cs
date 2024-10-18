using GestionCandidatosApi.ConexionDB;
using GestionCandidatosApi.Modelos;
using Microsoft.EntityFrameworkCore;

namespace GestionCandidatosApi.Services
{
    public interface IMenusService {
        Task<List<Menus>> GetAll(Filtros filtro);
    }
    public class MenusService : IMenusService
    {
        private readonly Context _dbContext;

        public MenusService(Context dbContext)
        {
            _dbContext = dbContext;
        }

        #region SELECT
        public async Task<List<Menus>> GetAll(Filtros filtro)
        {
            try
            {
                List<Menus> Menus = new List<Menus>();

                switch (filtro.FiltroPrimario)
                {
                    case "TODOS":
                        Menus = await _dbContext.Menus.ToListAsync();
                        break;
                    case "TODOSA": //menus activos
                        Menus = await _dbContext.Menus.Where(m => m.estado == "A").ToListAsync();
                        break;
                    case "NOMBRE": 
                        Menus = await _dbContext.Menus.Where(m => m.titulo.Contains(filtro.FiltroSecundario)).ToListAsync();
                        break;
                    case "CODIGO":
                        Menus = await _dbContext.Menus.Where(m => m.to == filtro.FiltroSecundario).ToListAsync();
                        break;
                    default:
                        Menus = await _dbContext.Menus.ToListAsync();
                        break;
                }


                return Menus;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion
    }
}
