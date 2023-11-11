using Identity.Persistence.Database;
using Identity.Service.Queries.DTOs;
using Microsoft.EntityFrameworkCore;
using Service.Common.Collection;
using Service.Common.Mapping;
using Service.Common.Paging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Service.Queries
{
    public interface IIdentityQueryService
    {
        Task<DataCollection<UsuarioDto>> GetAllAsync(int page, int take, IEnumerable<int> users = null);
        Task<UsuarioDto> GetAsync(int id);
    }

    public class IdentityQueryService: IIdentityQueryService
    {
        private ApplicationDbContext _context;
        
        public IdentityQueryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DataCollection<UsuarioDto>> GetAllAsync(int page, int take, IEnumerable<int> users = null)
        {
            var collection = await _context.Usuarios.Where(x => users == null || users.Contains(x.id))
                .OrderBy(x => x.id)
                .GetPagedAsync(page, take);

            return collection.MapTo<DataCollection<UsuarioDto>>();
        }

        public async Task<UsuarioDto> GetAsync(int id)
        {
            return (await _context.Usuarios.SingleAsync(x => x.id == id)).MapTo<UsuarioDto>();
        }
    }
}
