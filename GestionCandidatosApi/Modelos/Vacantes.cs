namespace GestionCandidatosApi.Modelos
{
    public class Vacantes
    {
        public int codigo_vacante { get; set; }
        public int codigo_puesto { get; set; }
        public int cantidad_puestos { get; set; }
        public DateTime fecha { get; set; }
        public string? usuario_ingresa { get; set; }
        public string? usuario_encargado { get; set; }
        public string? estado { get; set; }

    }
}
