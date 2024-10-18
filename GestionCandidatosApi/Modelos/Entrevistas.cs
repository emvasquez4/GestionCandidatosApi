namespace GestionCandidatosApi.Modelos
{
    public class Entrevistas
    {
        public int codigo_entrevista { get; set; }
        public int codigo_candidato { get; set; }
        public DateTime fecha { get; set; }
        public string? encargado { get; set; }
        public string? usuario_ingresa { get; set; }
        public string? estado { get; set; }

    }
}
