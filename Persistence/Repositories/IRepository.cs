using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
