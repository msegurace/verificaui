using Microsoft.EntityFrameworkCore;
using Service.Common.Collection;
using Service.Common.Mapping;
using Service.Common.Paging;
using System.Data;
using Token.Persistence.Database;
using Token.Service.Queries.DTOs;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Token.Service.Queries
{
    public interface ITokenQueryService
    {
        Task<DataCollection<Token2FADto>> GetAllAsync(int page, int take, IEnumerable<int> users = null);
        Task<DataCollection<Token2FADto>> GetAllForUserAsync(int idUser, int page, int take);
        Task<Token2FADto> GetAsync(int id);
        Task<Token2FADto> GetNewAsync(int idApp, int idUsr);
    }

    public class TokenQueryService: ITokenQueryService
    {
        private ApplicationDbContext _context;
        
        public TokenQueryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DataCollection<Token2FADto>> GetAllAsync(int page, int take, IEnumerable<int> users = null)
        {
            var collection = await _context.Tokens.Where(x => users == null || users.Contains(x.id))
                .OrderBy(x => x.id)
                .GetPagedAsync(page, take);

            return collection.MapTo<DataCollection<Token2FADto>>();
        }

        public async Task<DataCollection<Token2FADto>> GetAllForUserAsync(int idUser, int page, int take)
        {
            var collection = await _context.Tokens
               .Where(x => x.idusuario == idUser && !x.aceptado!.Value && !x.rechazado!.Value && x.creado!.Value.AddMinutes(5) > DateTime.Now )
               .OrderByDescending(x => x.creado)
               .GetPagedAsync(page, take);

            return collection.MapTo<DataCollection<Token2FADto>>();
        }

        public async Task<Token2FADto> GetAsync(int id)
        {
            return (await _context.Tokens.SingleAsync(x => x.id == id)).MapTo<Token2FADto>();
        }

        public async Task<Token2FADto> GetNewAsync(int idApp, int idUsr)
        {
            var token = (await _context.Tokens
                .Where(t => t.idaplicacion == idApp &&
                    t.idusuario == idUsr && t.creado > DateTime.Now.AddMinutes(-5))
                .OrderByDescending(o => o.id)
                .FirstOrDefaultAsync())?
                  .MapTo<Token2FADto>();
            return token;
        }
    }
}
