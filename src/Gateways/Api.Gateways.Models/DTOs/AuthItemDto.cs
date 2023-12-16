namespace Api.Gateways.Models.DTOs
{
    public class AuthItemDto
    {
        public int id { get; set; }
        public DateTime creado { get; set; }
        public DateTime expira { get; set; }
        public string? usuario { get; set; }
        public string? aplicacion { get; set; }
        public string? token { get; set; }
        public bool? aceptado { get; set; }
        public bool? rechazado { get; set; }
    }
}
