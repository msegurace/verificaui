using Application.Service.Queries.DTOs;
using Microsoft.EntityFrameworkCore;
using Service.Common.Collection;
using Service.Common.Mapping;
using Service.Common.Paging;
using System.Data;
using Application.Persistence.Database;

namespace Application.Service.Queries
{
    public interface IApplicationQueryService
    {
        Task<DataCollection<AplicacionDto>> GetAllAsync(int page, int take, IEnumerable<int> users = null);
        Task<AplicacionDto> GetAsync(int id);
    }

    public class ApplicationQueryService: IApplicationQueryService
    {
        private ApplicationDbContext _context;
        
        public ApplicationQueryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DataCollection<AplicacionDto>> GetAllAsync(int page, int take, IEnumerable<int> users = null)
        {
            var collection = await _context.Aplicaciones.Where(x => users == null || users.Contains(x.id))
                .OrderBy(x => x.id)
                .GetPagedAsync(page, take);

            return collection.MapTo<DataCollection<AplicacionDto>>();
        }

        public async Task<AplicacionDto> GetAsync(int id)
        {
            return (await _context.Aplicaciones.SingleAsync(x => x.id == id)).MapTo<AplicacionDto>();
        }
    }
}
