using GestionCandidatosApi.ConexionDB;
using GestionCandidatosApi.Modelos;
using Microsoft.EntityFrameworkCore;

namespace GestionCandidatosApi.Services
{
    public interface IRolesService
    {
        Task<List<Roles>> GetAll(Filtros filtro);

        Task<string> InsertRoles(Roles modelo);

        Task<int> UpdateRoles(Roles modelo);
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
        #endregion

        #region INSERT 
        public async Task<string> InsertRoles(Roles modelo)
        {

            try
            {
                modelo.descripcion = modelo.descripcion ?? "no data";
                modelo.estado = modelo.estado ?? "A"; // Valor por defecto

                await dbContext.Roles.AddAsync(modelo);
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
        public async Task<int> UpdateRoles(Roles modelo)
        {
            try
            {
                var ejecuta = 0; //verifica si existe
                var rol = await dbContext.Roles.Where(m => m.codigo_rol == modelo.codigo_rol).FirstOrDefaultAsync();
                if (rol != null)
                {
                    // Actualizar las propiedades de la entrevista
                    rol.descripcion = modelo.descripcion;
                    rol.estado = modelo.estado;

                    // Guardar los cambios en la base de datos
                    dbContext.Roles.Update(rol);
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
                throw new Exception("Error al insertar rol al sistema");
            }
        }

        #endregion

    }
}
