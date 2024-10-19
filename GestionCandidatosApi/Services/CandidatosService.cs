using GestionCandidatosApi.ConexionDB;
using GestionCandidatosApi.Modelos;
using Microsoft.EntityFrameworkCore;

namespace GestionCandidatosApi.Services
{
    public interface ICandidatosService
    {
        Task<List<Candidatos>> GetAll(Filtros filtro);
        Task<string> InsertCandidatos(Candidatos modelo);
        Task<int> UpdateCandidatos(Candidatos modelo);
        //  Task<Entrevistas> GetEntrevista(Entrevistas entrevista);
    }
    public class CandidatosService : ICandidatosService
    {
        private readonly Context dbContext;

        public CandidatosService(Context _dbContext)
        {
            dbContext = _dbContext;

        }

        #region INSERT 
        public async Task<string> InsertCandidatos(Candidatos modelo)
        {

            try
            {
                
                modelo.nombre = modelo.nombre ?? "no data";
                modelo.apellido = modelo.apellido ?? "no data";
                modelo.correo = modelo.correo ?? "no data";
                modelo.expectativa_salarial = modelo.expectativa_salarial ?? 0; // O el valor que consideres adecuado
                modelo.puesto_solicitado = modelo.puesto_solicitado ?? "no data";
                modelo.estado = modelo.estado ?? "A"; // Valor por defecto
                modelo.cargos_anteriores = modelo.cargos_anteriores ?? "no data";
                modelo.habilidades = modelo.habilidades ?? "no data";
                modelo.escolaridad = modelo.escolaridad ?? "no data";
                modelo.genero = modelo.genero ?? "no data";
                modelo.usuario_ingresa = modelo.usuario_ingresa?.ToUpper() ?? "no data"; // Convertir a mayúsculas si no es nulo
                modelo.usuario_actualiza = modelo.usuario_actualiza?.ToUpper(); // No se asigna un valor por defecto


                await dbContext.Candidatos.AddAsync(modelo);
                await dbContext.SaveChangesAsync();
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

        #region UPDATE 
        public async Task<int> UpdateCandidatos(Candidatos modelo)
        {
            try
            {
                var ejecuta = 0; //verifica si existe
                var candidato = await dbContext.Candidatos.Where(m => m.codigo_candidato == modelo.codigo_candidato).FirstOrDefaultAsync();
                if (candidato != null)
                {
                    // Actualizar las propiedades de la entrevista
                    candidato.nombre = modelo.nombre;
                    candidato.apellido = modelo.apellido;
                    candidato.fecha_nacimiento = modelo.fecha_nacimiento;
                    candidato.correo = modelo.correo;
                    candidato.expectativa_salarial = modelo.expectativa_salarial;
                    candidato.puesto_solicitado = modelo.puesto_solicitado;
                    candidato.estado = modelo.estado;
                    candidato.cargos_anteriores = modelo.cargos_anteriores;
                    candidato.habilidades = modelo.habilidades;
                    candidato.escolaridad = modelo.escolaridad;
                    candidato.genero = modelo.genero;
                    candidato.usuario_ingresa = modelo.usuario_ingresa;
                    candidato.usuario_actualiza = modelo.usuario_actualiza;

                    // Guardar los cambios en la base de datos
                    dbContext.Candidatos.Update(candidato);
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
        public async Task<List<Candidatos>> GetAll(Filtros filtro)
        {
            // Implementa el filtrado según los parámetros del filtro si es necesario
            return await dbContext.Candidatos.ToListAsync();
        }
        #endregion

    }
}
