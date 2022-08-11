using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCService
{
    public class AkamaiService : IAkamaiService
    {
        public Task<int> GetCount()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AkamaiUser>> GetList()
        {
            throw new NotImplementedException();
        }

        public Task RemoveUsers(List<string> ids)
        {
            throw new NotImplementedException();
        }
    }
}
