using Domain.Models;

namespace Persistence.Repositories
{
    public  interface ICosmosRepository
    {
        Task SaveAsync(List<AkamaiUser> users);

    }
}
