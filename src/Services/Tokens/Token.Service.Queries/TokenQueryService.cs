using Microsoft.EntityFrameworkCore;
using Service.Common.Collection;
using Service.Common.Mapping;
using Service.Common.Paging;
using System.Data;
using Token.Persistence.Database;
using Token.Service.Queries.DTOs;

namespace Token.Service.Queries
{
    public interface ITokenQueryService
    {
        Task<DataCollection<Token2FADto>> GetAllAsync(int page, int take, IEnumerable<int> users = null);
        Task<Token2FADto> GetAsync(int id);
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

        public async Task<Token2FADto> GetAsync(int id)
        {
            return (await _context.Tokens.SingleAsync(x => x.id == id)).MapTo<Token2FADto>();
        }
    }
}
