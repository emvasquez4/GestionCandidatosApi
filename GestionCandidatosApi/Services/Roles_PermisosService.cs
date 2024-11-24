using GestionCandidatosApi.ConexionDB;
using GestionCandidatosApi.Modelos;
using GestionCandidatosApi.Services.Utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionCandidatosApi.Services
{

    public interface IRolesPermisosService {
        Task<List<Roles_Permisos>> GetAll(Filtros filtro);
        Task<string> InsertRP(Roles_Permisos modelo);
    }
    public class Roles_PermisosService : IRolesPermisosService
    {
        private readonly Context _dbContext;

        public Roles_PermisosService(Context dbContext)
        {
            _dbContext = dbContext;
        }

        #region SELECT
        public async Task<List<Roles_Permisos>> GetAll(Filtros filtro)
        {
            try
            {
                List<Roles_Permisos> RolesP = new List<Roles_Permisos>();

                switch (filtro.FiltroPrimario)
                {
                    case "TODOS":
                        RolesP = await _dbContext.Roles_Permisos.ToListAsync();
                        break;
                    case "CODPERMISO": //codpermiso
                        RolesP = await _dbContext.Roles_Permisos.Where(m => m.codigo_permiso == filtro.FiltroSecundario).ToListAsync();
                        break;
                    case "CODIGOROL":
                        RolesP = await _dbContext.Roles_Permisos.Where(m => m.codigo_rol == filtro.FiltroSecundario).ToListAsync();
                        break;
                    default:
                        RolesP = await _dbContext.Roles_Permisos.ToListAsync();
                        break;
                }


                return RolesP;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region INSERT 
        public async Task<string> InsertRP(Roles_Permisos modelo)
        {

            try
            { 
                Roles_Permisos data = new Roles_Permisos();
                foreach(var item in modelo.permisos)
                {
                    data.codigo_rol = modelo.codigo_rol;
                    data.codigo_permiso = item;
                    data.estado = "A";

                    await _dbContext.Roles_Permisos.AddAsync(data);
                    await _dbContext.SaveChangesAsync();
                }
                
               
                
                //transaction.Commit();
                return "Exito";
            }
            catch (Exception e)
            {
                //transaction.Rollback();
                throw new Exception("Error al insertar permisos al rol");
            }
        }
        #endregion

        #region UPDATE 
        public async Task<int> Update(Roles_Permisos modelo)
        {
            try
            {
                var ejecuta = 0; //verifica si existe
                var usuario = await _dbContext.Roles_Permisos.Where(m => m.codigo_rol == modelo.codigo_rol).ToListAsync();
                if (usuario != null)
                {
                    
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
    }
}
