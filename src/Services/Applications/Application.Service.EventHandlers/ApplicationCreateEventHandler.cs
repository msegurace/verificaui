using Application.Persistence.Database;
using ApplicationDomain;
using MediatR;
using User.Service.EventHandlers.Commands;

namespace Application.Service.EventHandlers
{
    public class ApplicationCreateEventHandler: INotificationHandler<ApplicationCreateCommand>
    {
        private ApplicationDbContext _context;

        public ApplicationCreateEventHandler(ApplicationDbContext context) 
        { 
            _context = context;
        }

        public async Task Handle(ApplicationCreateCommand command, CancellationToken cancellationToken)
        {
            await _context.AddAsync(new Aplicacion
            {
                descripcion = command.descripcion,
                url = command.url,
                origen = command.origen,
                clasificacion_ens = command.clasificacion_ens
            });

            await _context.SaveChangesAsync();
        }
    }
}
