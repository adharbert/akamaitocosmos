using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCService
{
    public interface ICosmosDbService
    {
        Task<IEnumerable<AkamaiUser>> GetMultipleAsync(string query);
        Task<AkamaiUser> GetAsync(string id);
        Task AddAsync(AkamaiUser user);
        Task UpdateAsync(string id, AkamaiUser user);
        Task DeleteAsync(string id);
    }
}
