using GestionCandidatosApi.ConexionDB;
using GestionCandidatosApi.Modelos;
using GestionCandidatosApi.Services.Utilidades;
using Microsoft.EntityFrameworkCore;

namespace GestionCandidatosApi.Services
{
    public interface IUsuariosService{
        Task<List<Usuarios>> GetAll(Filtros filtro);
        Task<string> InsertUsuarios(Usuarios modelo);
        Task<int> UpdateUsuarios(Usuarios modelo);
        Task<Usuarios> GetUsuario(Usuarios user);
    }
    public class UsuariosService : IUsuariosService
    {

        private readonly Context dbContext;
        private readonly EncryptionService _encryptionService;

        public UsuariosService(Context _dbContext, EncryptionService encryptionService) { 
            dbContext = _dbContext;
            _encryptionService = encryptionService;
        }

        #region SELECT
        public async Task<List<Usuarios>> GetAll(Filtros filtro)
        {
            try
            {
                List<Usuarios> Usuarios = new List<Usuarios>();

                switch (filtro.FiltroPrimario) {
                    case "TODOS":
                        Usuarios = await dbContext.Usuarios.ToListAsync();
                        break;
                     case "TODOSA": //usuarios activos
                        Usuarios = await dbContext.Usuarios.Where(m => m.estado == "A").ToListAsync();
                        break;
                     case "NOMBRE": //USUARIOS POR NOMBRE
                        Usuarios = await dbContext.Usuarios.Where(m => m.nombre.Contains(filtro.FiltroSecundario)).ToListAsync();
                        break;
                    default:
                        Usuarios = await dbContext.Usuarios.ToListAsync();
                        break;
                }
                Usuarios = await dbContext.Usuarios.ToListAsync();

                return Usuarios;
            }
            catch (Exception e) {
                throw e;
            }
        }
        #endregion

        #region SELECT USER
        public async Task<Usuarios> GetUsuario(Usuarios user)
        {
            try
            {

                var usuario = await dbContext.Usuarios.Where(m => m.username == user.username && m.password == _encryptionService.Encrypt(user.password)).FirstOrDefaultAsync();

                if (usuario != null)
                {
                    return usuario;
                }
                else {
                    return null;
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region INSERT 
        public async Task<string> InsertUsuarios(Usuarios modelo)
        {

            //using (var transaction = dbContext.Database.BeginTransaction())
            //{
                try
                {
                    modelo.username = modelo.username != null ? modelo.username.ToUpper() : "no data";
                    modelo.estado = modelo.estado != null ? modelo.estado : "A";
                    modelo.password = _encryptionService.Encrypt(modelo.password);
                    modelo.id = 2;

                    await dbContext.Usuarios.AddAsync(modelo);
                    await dbContext.SaveChangesAsync();
                    //transaction.Commit();
                    return "Exito";
                }
                catch(Exception e)
                {
                    //transaction.Rollback();
                    throw new Exception("Error al insertar los usuarios al sistema");
                }
            
        }

        #endregion

        #region UPDATE 
        public async Task<int> UpdateUsuarios(Usuarios modelo)
        {
            try
            {
                var ejecuta = 0; //verifica si existe
                var usuario = await dbContext.Usuarios.Where(m => m.id == modelo.id).FirstOrDefaultAsync();
                if (usuario != null)
                {
                    if (_encryptionService.Decrypt(modelo.password) == usuario.password)
                    {
                        usuario.email = modelo.email;
                        usuario.estado = modelo.estado;
                    }
                    else {
                       
                        usuario.password = _encryptionService.Encrypt(modelo.password);
                       
                        dbContext.Usuarios.Add(usuario);
                        ejecuta = 0;
                    }
                }
                else {
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
