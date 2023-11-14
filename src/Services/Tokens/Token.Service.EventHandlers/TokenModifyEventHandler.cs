using MediatR;
using Token.Persistence.Database;
using Token.Service.EventHandlers.Commands;
using TokenDomain;

namespace Token.Service.EventHandlers
{
    public class TokenModifyEventHandler: INotificationHandler<TokenModifyCommand>
    {
        private ApplicationDbContext _context;

        public TokenModifyEventHandler(ApplicationDbContext context) 
        { 
            _context = context;
        }

        public async Task Handle(TokenModifyCommand command, CancellationToken cancellationToken)
        {
            _context.Update(new Token2FA
            {
                id = command.id,
                aceptado = command.aceptado,
                idaplicacion = command.idaplicacion,
                idusuario = command.idusuario,
                rechazado = command.rechazado,
                token = command.token
            });

            await _context.SaveChangesAsync();
        }
    }
}
