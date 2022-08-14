using Domain;

namespace Application.Services
{
    internal interface IMigrateDataService
    {
        Task<TransactionResultDto> Transfer();
    }
}
