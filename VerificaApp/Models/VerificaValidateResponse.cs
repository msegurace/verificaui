
using System.Text.Json.Serialization;

namespace VerificaApp.Models
{
    public class VerificaAppGenericResponse
    {
        public string code { get; set; }
        public Object content { get; set; }
    }

    //TODO: Ver para qué sirve esta clase
    [JsonSerializable(typeof(VerificaAppGenericResponse))]
    internal sealed partial class VerificaAppGenericResponseContext : JsonSerializerContext
    {

    }
}
