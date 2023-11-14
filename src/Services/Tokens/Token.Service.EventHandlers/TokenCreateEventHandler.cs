using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text;
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
            try
            {
                var token = GenerateToken();
                await _context.AddAsync(new Token2FA
                {
                    creado = DateTime.Now,
                    aceptado = false,
                    idaplicacion = command.idaplicacion,
                    idusuario = command.idusuario,
                    rechazado = false,
                    token = token
                });

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private string GenerateToken()
        {
            //Longitud del token
            int i = 256;
            String theAlphaNumericS;
            StringBuilder builder;

            theAlphaNumericS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
                                        + "0123456789";

            //create the StringBuffer
            builder = new StringBuilder(i);

            Random r = new Random();

            for (int m = 0; m < i; m++)
            {

                // generate numeric
                int myindex
                    = r.Next(theAlphaNumericS.Length - 1);

                // add the characters
                builder.Append(theAlphaNumericS[myindex]);
            }

            return builder.ToString();
        }
    }
}
