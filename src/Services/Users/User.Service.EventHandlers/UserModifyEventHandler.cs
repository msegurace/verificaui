using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Service.EventHandlers.Commands;
using UserDomain;
using Users.Persistence.Database;

namespace User.Service.EventHandlers
{
    public class UserModifyEventHandler: INotificationHandler<UserModifyCommand>
    {
        private ApplicationDbContext _context;

        public UserModifyEventHandler(ApplicationDbContext context) 
        { 
            _context = context;
        }

        public async Task Handle(UserModifyCommand command, CancellationToken cancellationToken)
        {
            _context.Update(new Usuario
            {
                id = command.id,
                nombre = command.nombre,
                apellido1 = command.apellido1,
                apellido2 = command.apellido2,
                email = command.email,
                guid = command.guid,
                telefono = command.telefono,
                username = command.username,
                password = command.password,
                admin = command.admin
            });

            await _context.SaveChangesAsync();
        }
    }
}
