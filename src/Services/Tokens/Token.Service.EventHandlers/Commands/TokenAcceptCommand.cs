using MediatR;

namespace Token.Service.EventHandlers.Commands
{
    public class TokenAcceptCommand: INotification
    {
        public int id { get; set; }
    }
}
