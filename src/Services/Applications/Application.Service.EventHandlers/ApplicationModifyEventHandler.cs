using Application.Persistence.Database;
using ApplicationDomain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Service.EventHandlers.Commands;

namespace User.Service.EventHandlers
{
    public class ApplicationModifyEventHandler: INotificationHandler<ApplicationModifyCommand>
    {
        private ApplicationDbContext _context;

        public ApplicationModifyEventHandler(ApplicationDbContext context) 
        { 
            _context = context;
        }

        public async Task Handle(ApplicationModifyCommand command, CancellationToken cancellationToken)
        {
            _context.Update(new Aplicacion
            {
                id = command.id,
                descripcion = command.descripcion,
                url = command.url,
                origen = command.origen,
                clasificacion_ens = command.clasificacion_ens
            });

            await _context.SaveChangesAsync();
        }
    }
}
