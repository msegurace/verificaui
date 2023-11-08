using Microsoft.EntityFrameworkCore;
using Service.Common.Collection;
using Service.Common.Mapping;
using Service.Common.Paging;
using System.Data;
using Users.Persistence.Database;
using Users.Service.Queries.DTOs;

namespace Users.Service.Queries
{
    public interface IUsuarioQueryService
    {
        Task<DataCollection<UsuarioDto>> GetAllAsync(int page, int take, IEnumerable<int> users = null);
        Task<UsuarioDto> GetAsync(int id);
    }

    public class UsuarioQueryService: IUsuarioQueryService
    {
        private ApplicationDbContext _context;
        
        public UsuarioQueryService(ApplicationDbContext context)
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
