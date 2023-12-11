using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace VerificaApp.Models
{
    public class AuthRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string uid { get; set; }
        public string token { get; set; }
        public bool aceptado { get; set; }
        public string aplicacion { get; set; }
        public string ip_origen { get; set; }
        public long? fecha_creacion { get; set; }
        public long? expira { get; set; }
    }

    //TODO: Ver para qué sirve esta clase
    [JsonSerializable(typeof(List<AuthRequest>))]
    internal sealed partial class AuthRequestContext : JsonSerializerContext
    {

    }
}