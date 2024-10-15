namespace GestionCandidatosApi.Modelos
{
    public class Candidatos
    {
        public int codigo_candidato { get; set; }
        public string? nombre { get; set; }

        public string? apellido { get; set; }
        public DateTime fecha_nacimiento { get; set; }
        public string? correo { get; set; }
        public double? expectativa_salarial { get; set; }
        public string? puesto_solicitado { get; set; }
        public string? estado { get; set; }
        public string? cargos_anteriores { get; set; }
        public string? habilidades { get; set; }
        public string? escolaridad { get; set; }
        public string? genero { get; set; }
        public string? usuario_ingresa { get; set; }
        public string? usuario_actualiza { get; set; }

    }
}
