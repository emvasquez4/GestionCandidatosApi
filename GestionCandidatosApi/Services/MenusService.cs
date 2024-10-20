using GestionCandidatosApi.ConexionDB;
using GestionCandidatosApi.Modelos;
using Microsoft.EntityFrameworkCore;

namespace GestionCandidatosApi.Services
{
    public interface IMenusService {
        Task<List<Menus>> GetAll(Filtros filtro);

        Task<string> InsertMenus(Menus modelo);
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

        #region INSERT 
        public async Task<string> InsertMenus(Menus modelo)
        {

            try
            {
                modelo.titulo = modelo.titulo ?? "no data";
                modelo.estado = modelo.estado ?? "A"; // Valor por defecto

                await _dbContext.Menus.AddAsync(modelo);
                await _dbContext.SaveChangesAsync();
                //transaction.Commit();
                return "Exito";
            }
            catch (Exception e)
            {
                //transaction.Rollback();
                throw new Exception("Error al insertar candidatos");
            }
        }
        #endregion
    }
}
