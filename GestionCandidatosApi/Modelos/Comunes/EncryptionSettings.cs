using System.ComponentModel.DataAnnotations.Schema;

namespace GestionCandidatosApi.Modelos
{
    [NotMapped]
    public class EncryptionSettings
    {
        public string Key { get; set; }
        public string IV { get; set; }
    }
}
