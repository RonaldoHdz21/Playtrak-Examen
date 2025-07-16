using api.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using api.Data;
using Microsoft.Extensions.Configuration;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using api.Services.Implements;

namespace api.Services
{
    public class BalanceService : IBalanceService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly LogManager logManager;
        private readonly IBalanceHistoryService _balanceHistoryService;
        public BalanceService(AppDbContext context, IConfiguration configuration, IBalanceHistoryService balanceHistoryService)
        {
            _context = context;
            _configuration = configuration;
            logManager = new LogManager(_configuration);
            _balanceHistoryService = balanceHistoryService;
        }
        public async Task<Balance> GetBalanceByIdAsync(int id)
        {
            string message = "";
            string methodName = "Get balance by id";
            Balance balance;
            try
            {
                balance = await _context.Balances
                    .Where(c => c.ClientId == id && c.Active == true).FirstOrDefaultAsync();

                if (balance == null)
                    throw new Exception("Client not found.");


                message = $"Client {balance.ClientId} balances getted";
                logManager.AddLog(message, methodName, "Ok");

                return balance;
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
        public async Task<Balance> AddBalanceAsync(int clientId)
        {
            string message = "";
            string methodName = "Add balance";
            try
            {
                Balance balance = new Balance()
                {
                    ClientId = clientId,
                    BalanceAmount = 0,
                    Active = true
                };
                _context.Balances.Add(balance);
                await _context.SaveChangesAsync();

                message = $"Client {balance.ClientId} balances added";
                logManager.AddLog(message, methodName, "Ok");

                return balance;
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

        public async Task<Balance> UpdateBalanceAsync(int clientId, double Amount)
        {
            string message = "";
            string methodName = "Update balance";
            try
            {
                Balance balance = await _context.Balances.FindAsync(clientId);
                if (balance == null || balance.Active == false)
                    throw new Exception("Client not found.");

                double oldAmount = balance.BalanceAmount;
                double newAmount = balance.BalanceAmount + Amount;

                balance.BalanceAmount = newAmount;

                _context.Balances.Update(balance);
                await _context.SaveChangesAsync();

                //Add balance history
                await _balanceHistoryService.AddBalanceHistoryAsync(clientId, oldAmount, newAmount);

                message = $"Client {balance.ClientId} balances updated";
                logManager.AddLog(message, methodName, "Ok");

                return balance;
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
        public async Task<Balance> DeleteBalanceAsync(int clientId)
        {
            string message = "";
            string methodName = "Delete balance";
            try
            {
                Balance balance = await _context.Balances.FindAsync(clientId);
                if (balance == null)
                    throw new Exception("Client not found.");

                balance.Active = false;

                _context.Balances.Update(balance);
                await _context.SaveChangesAsync();

                message = $"Client {balance.ClientId} balances deleted";
                logManager.AddLog(message, methodName, "Ok");

                return balance;
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
