using api.Models;
using System.Threading.Tasks;

namespace api.Services.Implements
{
    public interface IBalanceHistoryService
    {
        Task<BalanceHistory> AddBalanceHistoryAsync(int clientId, double oldAmount, double newAmount);
    }
}
