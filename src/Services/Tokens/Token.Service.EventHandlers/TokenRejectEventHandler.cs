using MediatR;
using System.Diagnostics;
using Token.Persistence.Database;
using Token.Service.EventHandlers.Commands;
using TokenDomain;

namespace Token.Service.EventHandlers
{
    public class TokenRejectEventHandler: INotificationHandler<TokenRejectCommand>
    {
        private ApplicationDbContext _context;

        public TokenRejectEventHandler(ApplicationDbContext context) 
        { 
            _context = context;
        }

        public async Task Handle(TokenRejectCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var token = _context.Tokens.Single(t => t.id == command.id);
                token.rechazado = true;
                _context.Update(token);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Debug.Write(ex.InnerException?.ToString());
                throw;
            }
        }
    }
}
