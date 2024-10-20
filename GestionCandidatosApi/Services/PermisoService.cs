using GestionCandidatosApi.ConexionDB;
using GestionCandidatosApi.Modelos;
using Microsoft.EntityFrameworkCore;

namespace GestionCandidatosApi.Services
{
    public interface IPermisosService {
        Task<List<Permiso>> GetAll(Filtros filtro);
        Task<string> InsertPermisos(Permiso modelo);
        Task<int> UpdatePermisos(Permiso modelo);
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

        #region INSERT 
        public async Task<string> InsertPermisos(Permiso modelo)
        {

            try
            {
                modelo.descripcion = modelo.descripcion ?? "no data";
                modelo.estado = modelo.estado ?? "A"; // Valor por defecto
               
                await dbContext.Permisos.AddAsync(modelo);
                await dbContext.SaveChangesAsync();
               
                return "Exito";
            }
            catch (Exception e)
            { 
                throw new Exception("Error al insertar candidatos");
            }
        }
        #endregion

        #region UPDATE 
        public async Task<int> UpdatePermisos(Permiso modelo)
        {
            try
            {
                var ejecuta = 0; //verifica si existe
                var permiso = await dbContext.Permisos.Where(m => m.codigo_permiso == modelo.codigo_permiso).FirstOrDefaultAsync();
                if (permiso != null)
                {
                    // Actualizar las propiedades de la entrevista
                    permiso.descripcion = modelo.descripcion;
                    permiso.estado = modelo.estado;

                    // Guardar los cambios en la base de datos
                    dbContext.Permisos.Update(permiso);
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
                throw new Exception("Error al insertar permiso al sistema");
            }
        }

        #endregion
    }

}
