

namespace Api.Gateways.Models.DTOs
{ 
    public class Token2FADto
    {
        public int id { get; set; }
        public DateTime creado { get; set; }
        public int idusuario { get; set; }
        public int idaplicacion { get; set; }
        public string token { get; set; }
        public bool aceptado { get; set; }
        public bool rechazado { get; set; }

    }
}
