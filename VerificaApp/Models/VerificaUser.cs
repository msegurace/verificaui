using System.Text.Json.Serialization;

namespace VerificaApp.Models
{
    /// <summary>
    /// Representa al usuario conectado y se envía en el cuerpo del mensaje en las llamadas a la API
    /// </summary>
    public class VerificaAppUser
    {
        public int id { get; set; }
        public string uid { get; set; }
        public string phone { get; set; }
        public string password { get; set; }
        public bool? registering { get; set; }
        public string token { get; set; }
        public Guid guid { get; set; }
        public string firebase { get; set; }
        public string otp { get; set; }
    }

    //TODO: Ver para qué sirve esta clase
    [JsonSerializable(typeof(VerificaAppUser))]
    internal sealed partial class VerificaAppUserContext : JsonSerializerContext
    {

    }
}
