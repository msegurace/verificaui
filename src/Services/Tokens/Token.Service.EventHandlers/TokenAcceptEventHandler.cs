using MediatR;
using System.Diagnostics;
using Token.Persistence.Database;
using Token.Service.EventHandlers.Commands;
using TokenDomain;

namespace Token.Service.EventHandlers
{
    public class TokenAcceptEventHandler: INotificationHandler<TokenAcceptCommand>
    {
        private ApplicationDbContext _context;

        public TokenAcceptEventHandler(ApplicationDbContext context) 
        { 
            _context = context;
        }

        public async Task Handle(TokenAcceptCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var token = _context.Tokens.Single(t => t.id == command.id);
                token.aceptado = true;
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
