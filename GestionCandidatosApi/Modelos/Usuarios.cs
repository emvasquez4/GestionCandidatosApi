namespace GestionCandidatosApi.Modelos
{
    public class Usuarios
    {
        public int  id { get; set; }
        public string? nombre { get; set; }
        public string? apellido { get; set; }
        public string? email { get; set; }
        public string? password { get; set; }
        public string? password2 { get; set; }
        public string? estado { get; set; }
        public DateTime? fecha_creacion { get; set; }
    }
}
