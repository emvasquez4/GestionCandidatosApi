namespace GestionCandidatosApi.Modelos
{
    public class Permiso
    {
        public string? codigo_permiso { get; set; }
        public string? descripcion { get; set; }

        public string? estado { get; set; }

    }

    public class PermisoSalida { 
        public bool nuevo { get; set; }
        public bool actualizar { get; set; }
        public bool eliminar { get; set; }
        public bool consultar { get; set; }
        public bool pdf { get; set; }

    }
}
