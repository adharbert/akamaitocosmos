using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCService
{
    public class CosmosService : ICosmosDbService
    {

        private readonly IDbContextFactory<AkamaiContext> _contextFactory;

        public CosmosService(IDbContextFactory<AkamaiContext> contextFactory) 
        {
            this._contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
        }

        public Task AddAsync(AkamaiUser user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<AkamaiUser> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<AkamaiUser> GetByAmsId(int CustomerNumber)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AkamaiUser>> GetMultipleAsync(string query)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(string id, AkamaiUser user)
        {
            throw new NotImplementedException();
        }
    }
}
