using GestionCandidatosApi.ConexionDB;
using GestionCandidatosApi.Modelos;
using Microsoft.EntityFrameworkCore;

namespace GestionCandidatosApi.Services
{
    public interface IPuestosService { 
    
    }
    public class PuestosService : IPuestosService
    {
        private readonly Context dbContext;

        public PuestosService(Context _dbcontext)
        {
            dbContext = _dbcontext;
        }

        #region SELECT
        public async Task<List<Puestos>> GetAll(Filtros filtro)
        {
            try
            {
                List<Puestos> Puestos = new List<Puestos>();

                switch (filtro.FiltroPrimario)
                {
                    case "TODOS":
                        Puestos = await dbContext.Puestos.ToListAsync();
                        break;
                    case "TODOSA": 
                        Puestos = await dbContext.Puestos.Where(m => m.estado == "A").ToListAsync();
                        break;
                    case "NOMBRE":
                        Puestos = await dbContext.Puestos.Where(m => m.descripcion_puesto.Contains(filtro.FiltroSecundario)).ToListAsync();
                        break;
                    case "CODIGO":
                        Puestos = await dbContext.Puestos.Where(m => m.codigo_puesto == Convert.ToInt32(filtro.FiltroSecundario)).ToListAsync();
                        break;
                    default:
                        Puestos = await dbContext.Puestos.ToListAsync();
                        break;
                }


                return Puestos;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion
    }
}
