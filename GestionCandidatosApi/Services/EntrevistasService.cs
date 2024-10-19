using GestionCandidatosApi.ConexionDB;
using GestionCandidatosApi.Modelos;
using GestionCandidatosApi.Services.Utilidades;
using Microsoft.EntityFrameworkCore;

namespace GestionCandidatosApi.Services
{
    public interface IEntrevistasService
    {
        Task<List<Entrevistas>> GetAll(Filtros filtro);
        Task<string> InsertEntrevistas(Entrevistas modelo);
        Task<int> UpdateEntrevistas(Entrevistas modelo);
      //  Task<Entrevistas> GetEntrevista(Entrevistas entrevista);

    }

    public class EntrevistasService : IEntrevistasService
    {
        private readonly Context dbContext;
       

        public EntrevistasService(Context _dbContext)
        {
            dbContext = _dbContext;
           
        }

        #region INSERT 
        public async Task<string> InsertEntrevistas(Entrevistas modelo)
        {

            try
            {
                modelo.fecha = DateTime.Now;
                modelo.encargado = modelo.encargado != null ? modelo.encargado.ToUpper() : "no data";
                modelo.usuario_ingresa = modelo.usuario_ingresa != null ? modelo.usuario_ingresa.ToUpper() : "no data";
                modelo.estado = modelo.estado != null ? modelo.estado : "A";
                

                await dbContext.Entrevistas.AddAsync(modelo);
                await dbContext.SaveChangesAsync();
                //transaction.Commit();
                return "Exito";
            }
            catch (Exception e)
            {
                //transaction.Rollback();
                throw new Exception("Error al intentar programar entrevista");
            }
        }
        #endregion

        #region UPDATE 
        public async Task<int> UpdateEntrevistas(Entrevistas modelo)
        {
            try
            {
                var ejecuta = 0; //verifica si existe
                var entrevista = await dbContext.Entrevistas.Where(m => m.codigo_entrevista == modelo.codigo_entrevista).FirstOrDefaultAsync();
                if (entrevista != null)
                {
                    // Actualizar las propiedades de la entrevista
                    entrevista.encargado = modelo.encargado;
                    entrevista.fecha = modelo.fecha;
                    entrevista.usuario_ingresa = modelo.usuario_ingresa;
                    entrevista.estado = modelo.estado;

                    // Guardar los cambios en la base de datos
                    dbContext.Entrevistas.Update(entrevista);
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
                throw new Exception("Error al insertar los usuarios al sistema");
            }
        }

        #endregion

        #region GETALL
        public async Task<List<Entrevistas>> GetAll(Filtros filtro)
        {
            // Implementa el filtrado según los parámetros del filtro si es necesario
            return await dbContext.Entrevistas.ToListAsync();
        }
        #endregion


    }
}
