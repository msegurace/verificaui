using MediatR;

namespace Token.Service.EventHandlers.Commands
{
    public class TokenCreateCommand: INotification
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
