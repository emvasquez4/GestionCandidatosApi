using GestionCandidatosApi.ConexionDB;
using GestionCandidatosApi.Modelos;
using Microsoft.EntityFrameworkCore;

namespace GestionCandidatosApi.Services
{
    public interface IPuestosService {
        Task<List<Puestos>> GetAll(Filtros filtro);
        Task<string> InsertPuestos(Puestos modelo);
        Task<int> UpdatePuestos(Puestos modelo);

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

        #region INSERT 
        public async Task<string> InsertPuestos(Puestos modelo)
        {

            try
            {
                modelo.nombre = modelo.nombre ?? "no data";
                modelo.descripcion_puesto = modelo.descripcion_puesto ?? "no data";
                modelo.usuario_ingresa = modelo.usuario_ingresa ?? "no data";
                modelo.estado = modelo.estado ?? "A"; // Valor por defecto

                await dbContext.Puestos.AddAsync(modelo);
                await dbContext.SaveChangesAsync();
                //transaction.Commit();
                return "Exito";
            }
            catch (Exception e)
            {
                //transaction.Rollback();
                throw new Exception("Error al insertar puesto");
            }
        }
        #endregion

        #region UPDATE 
        public async Task<int> UpdatePuestos(Puestos modelo)
        {
            try
            {
                var ejecuta = 0; //verifica si existe
                var puesto = await dbContext.Puestos.Where(m => m.codigo_puesto == modelo.codigo_puesto).FirstOrDefaultAsync();
                if (puesto != null)
                {
                    // Actualizar las propiedades del puesto
                    puesto.nombre = modelo.nombre;
                    puesto.descripcion_puesto = modelo.descripcion_puesto;
                    puesto.usuario_ingresa = modelo.usuario_ingresa;
                    puesto.estado = modelo.estado;

                    // Guardar los cambios en la base de datos
                    dbContext.Puestos.Update(puesto);
                    ejecuta = await dbContext.SaveChangesAsync();
                }
                else
                {
                    ejecuta = 1;
                }

                return ejecuta;
            }
            catch (Exception e)
            {
                throw new Exception("Error al insertar puesto al sistema");
            }
        }

        #endregion
    }
}
