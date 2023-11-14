using MediatR;

namespace Token.Service.EventHandlers.Commands
{
    public class TokenCreateCommand: INotification
    {
        public int idusuario { get; set; }
        public int idaplicacion { get; set; }
    }
}
