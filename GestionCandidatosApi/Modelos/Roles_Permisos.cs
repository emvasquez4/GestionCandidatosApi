namespace GestionCandidatosApi.Modelos
{
    public class Roles_Permisos
    {
        public string? codigo_rol { get; set; }
        public string? codigo_permiso { get; set; }
        public string? estado {  get; set; }

        public List<string> permisos { get; set; }

    }
}
