using MediatR;

namespace Token.Service.EventHandlers.Commands
{
    public class TokenRejectCommand: INotification
    {
        public int id { get; set; }
    }
}
