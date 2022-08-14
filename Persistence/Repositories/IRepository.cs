using Domain.Models;

namespace Persistence.Repositories
{
    public interface IRepository
    {
        Task<IEnumerable<AkamaiUser>> GetAllUsersAsync(string startingId, int totalRecordCount);
        Task<AkamaiUser> GetByIdAsync(object id);
        Task InsertAsync(AkamaiUser user);
        Task UpdateAsync(AkamaiUser user);
        Task Delete(AkamaiUser user);
        Task<int> CountAsync();
        Task SaveAsync();
    }
}
