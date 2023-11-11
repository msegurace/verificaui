using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Token.Persistence.Database;
using Token.Service.EventHandlers.Commands;
using TokenDomain;

namespace Token.Service.EventHandlers
{
    public class TokenCreateEventHandler: INotificationHandler<TokenCreateCommand>
    {
        private ApplicationDbContext _context;

        public TokenCreateEventHandler(ApplicationDbContext context) 
        { 
            _context = context;
        }

        public async Task Handle(TokenCreateCommand command, CancellationToken cancellationToken)
        {
            await _context.AddAsync(new Token2FA
            {
                 creado = command.creado,
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
