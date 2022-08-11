using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCService
{
    public interface IAkamaiService
    {
        Task<int> GetCount();
        Task<IEnumerable<AkamaiUser>> GetList();

        Task RemoveUsers(List<string> ids);


    }
}
