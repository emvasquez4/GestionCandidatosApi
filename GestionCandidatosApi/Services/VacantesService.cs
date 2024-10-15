using GestionCandidatosApi.Modelos;
using GestionCandidatosApi.ConexionDB;
using Microsoft.EntityFrameworkCore;

namespace GestionCandidatosApi.Services
{
    public interface IVacantesService
    {
        Task<List<Vacantes>> GetAll(Filtros filtro);    // Obtener todas las vacantes con filtros
        Task<string> InsertVacantes(Vacantes modelo);   // Insertar una nueva vacante
        Task<int> UpdateVacantes(Vacantes modelo);      // Actualizar una vacante
        Task<Vacantes> GetVacante(Filtros filtro);      // Obtener una vacante específica
    }

    public class VacantesService : IVacantesService
    {
        private readonly Context dbContext;

        public VacantesService(Context _dbContext)
        {
            dbContext = _dbContext;
        }

        #region SELECT
        // Método para obtener todas las vacantes con filtros aplicados
        public async Task<List<Vacantes>> GetAll(Filtros filtro)
        {
            try
            {
                List<Vacantes> vacantes = new List<Vacantes>();

                // Ejemplo básico de filtro por algún campo de "filtro"
                switch (filtro.FiltroPrimario)
                {
                    default:
                        vacantes = await dbContext.Vacantes.ToListAsync();
                        break;
                }

                return vacantes;
            }
            catch (Exception e)
            {
                throw new Exception("Error al obtener las vacantes", e);
            }
        }

        // Método para obtener una vacante específica según el filtro
        public async Task<Vacantes> GetVacante(Filtros filtro)
        {
            try
            {
                var vacante = await dbContext.Vacantes.FirstOrDefaultAsync(v => v.codigo_vacante == Convert.ToInt32(filtro.FiltroPrimario));

                if (vacante == null)
                {
                    throw new Exception("Vacante no encontrada");
                }

                return vacante;
            }
            catch (Exception e)
            {
                throw new Exception("Error al obtener la vacante", e);
            }
        }
        #endregion

        #region INSERT 
        // Método para insertar una nueva vacante en la base de datos
        public async Task<string> InsertVacantes(Vacantes modelo)
        {
            try
            {
                // Asegurar que los campos estén en el formato correcto o asignar valores por defecto
                modelo.usuario_ingresa = modelo.usuario_ingresa?.ToUpper() ?? "NO DATA";
                modelo.usuario_encargado = modelo.usuario_encargado ?? "Disponible";

                await dbContext.Vacantes.AddAsync(modelo);
                await dbContext.SaveChangesAsync();  // Asegurarse de que la operación sea asíncrona

                return "Éxito";
            }
            catch (Exception e)
            {
                throw new Exception("Error al insertar vacantes al sistema", e);
            }
        }
        #endregion

        #region UPDATE 
        // Método para actualizar una vacante existente
        public async Task<int> UpdateVacantes(Vacantes modelo)
        {
            try
            {
                // Verificar si la vacante existe en la base de datos
                var vacante = await dbContext.Vacantes.FirstOrDefaultAsync(v => v.codigo_vacante == modelo.codigo_vacante);

                if (vacante == null)
                {
                    return 1;  // Vacante no encontrada
                }

                // Actualizar los campos de la vacante con la nueva información
                vacante.usuario_ingresa = modelo.usuario_ingresa?.ToUpper() ?? vacante.usuario_ingresa;
                vacante.usuario_encargado = modelo.usuario_encargado ?? vacante.usuario_encargado;

                // Guardar los cambios en la base de datos
                await dbContext.SaveChangesAsync();

                return 0;  // Vacante actualizada correctamente
            }
            catch (Exception e)
            {
                throw new Exception("Error al actualizar las vacantes en el sistema", e);
            }
        }
        #endregion
    }
}
