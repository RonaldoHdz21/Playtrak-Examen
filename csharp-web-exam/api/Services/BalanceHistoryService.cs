using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System;
using api.Services.Implements;

namespace api.Services
{
    public class BalanceHistoryService : IBalanceHistoryService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly LogManager logManager;
        public BalanceHistoryService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            logManager = new LogManager(_configuration);
        }

        public async Task<BalanceHistory> AddBalanceHistoryAsync(int clientId, double oldAmount, double newAmount)
        {
            string message = "";
            string methodName = "Add balance history";
            try
            {
                BalanceHistory balanceHistory = new BalanceHistory()
                {
                    ClientId = clientId,
                    OldAmount = oldAmount,
                    NewAmount = newAmount,
                    DateTime = DateTime.UtcNow
                };
                _context.BalancesHistory.Add(balanceHistory);
                await _context.SaveChangesAsync();

                message = $"Client {balanceHistory.ClientId} balances history added";
                logManager.AddLog(message, methodName, "Ok");

                return balanceHistory;
            }
            catch (DbUpdateException ex)
            {
                message = $"Database error: {ex.Message} - {ex.InnerException.ToString().Split("\n")[0]}";

                logManager.AddLog(message, methodName, "SQLServerError");

                throw new Exception(message);
            }
            catch (Exception ex)
            {
                message = $"Error: {ex.Message}";

                logManager.AddLog(message, methodName, "ApiError");

                throw new Exception(message);
            }
        }
    }
}
