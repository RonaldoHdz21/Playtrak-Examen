using api.Models;
using System.Threading.Tasks;

namespace api.Services.Implements
{
    public interface IBalanceService
    {
        Task<Balance> GetBalanceByIdAsync(int id);
        Task<Balance> AddBalanceAsync(int clientId);
        Task<Balance> UpdateBalanceAsync(int clientId, double Amount);
        Task<Balance> DeleteBalanceAsync(int clientId);
    }
}
