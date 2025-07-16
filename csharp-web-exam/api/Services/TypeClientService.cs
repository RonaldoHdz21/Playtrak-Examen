using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using api.Services.Implements;

namespace api.Services
{
    public class TypeClientService : ITypeClientService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly LogManager logManager;
        public TypeClientService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            logManager = new LogManager(_configuration);
        }

        public async Task<List<TypeClient>> GetTypeClientsAsync()
        {
            string message = "";
            string methodName = "Get type clients";
            List<TypeClient> typeClients;
            try
            {
                typeClients = await _context.TypesClients.ToListAsync();

                message = "Type clients getted";
                logManager.AddLog(message, methodName, "Ok");

                return typeClients;
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
        public async Task<TypeClient> GetTypeClientByIdAsync(int id)
        {
            string message = "";
            string methodName = "Get type clients by id";
            TypeClient typeClient;
            try
            {
                typeClient = await _context.TypesClients
                    .Where(c => c.Id == id).FirstOrDefaultAsync();

                if (typeClient == null)
                    throw new Exception("Type client not found.");


                message = $"Type client {typeClient.Description} getted";
                logManager.AddLog(message, methodName, "Ok");

                return typeClient;
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
