using api.Data;
using api.Models;
using api.Services.Implements;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace api.Services
{

    public class ClientService : IClientService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly LogManager logManager;
        private readonly IBalanceService _balanceService;
        public ClientService(AppDbContext context, IConfiguration configuration, IBalanceService balanceService) 
        {
            _context = context;
            _configuration = configuration; 
            logManager = new LogManager(_configuration);
            _balanceService = balanceService;
        }

        public async Task<List<Client>> GetClientsAsync() {
            string message = "";
            string methodName = "Get clients";
            List<Client> clients;
            try
            {
                clients = await _context.Clients
                    .Where(a => a.Active == true).ToListAsync();

                message = "Clients getted";
                logManager.AddLog(message, methodName, "Ok");

                return clients;
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

        public async Task<Client> GetClientByIdAsync(int id) {
            string message = "";
            string methodName = "Get clients by id";
            Client client;
            try
            {
                client = await _context.Clients
                    .Where(c => c.Id == id && c.Active == true).FirstOrDefaultAsync();

                if (client == null)
                    throw new Exception("Client not found.");
                

                message = $"Client {client.Id} - {client.Name} - {client.Email} getted";
                logManager.AddLog(message, methodName, "Ok");

                return client;
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

        public async Task<Client> AddClientAsync(Client client)
        {
            string message = "";
            string methodName = "Add client";
            try
            {
                if ((_context.Clients.Where(a => a.Name == client.Name && a.Email == client.Email && a.Active == true).Count()) > 0)
                    throw new Exception("Cliente ya existe.");

                _context.Clients.Add(client);
                await _context.SaveChangesAsync();

                //Add Balance 0
                await _balanceService.AddBalanceAsync(client.Id);

                message = $"Client {client.Id} - {client.Name} - {client.Email} added";
                logManager.AddLog(message, methodName, "Ok");

                return client;
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

        public async Task<Client> UpdateClientAsync(int id, [FromBody]Client clientMod)
        {
            string message = "";
            string methodName = "Update client";
            try
            {
                Client client = await _context.Clients.FindAsync(id);
                if (client == null)
                    throw new Exception("Client not found.");

                client.Name = clientMod.Name;
                client.Telephone = clientMod.Telephone;
                client.Email = clientMod.Email;
                client.Birthdate = clientMod.Birthdate;
                client.Type = clientMod.Type;
                client.Active = clientMod.Active;

                _context.Clients.Update(client);
                await _context.SaveChangesAsync();

                message = $"Client {client.Id} - {client.Name} - {client.Email} updated";
                logManager.AddLog(message, methodName, "Ok");

                return client;
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
        public async Task<Client> DeleteClientAsync(int id)
        {
            string message = "";
            string methodName = "Delete client by id";
            try
            {
                Client client = await _context.Clients.FindAsync(id);

                if (client == null)
                    throw new Exception("Client not found.");

                client.Active = false;
                _context.Clients.Update(client);
                await _context.SaveChangesAsync();

                //Delete balance
                await _balanceService.DeleteBalanceAsync(id);

                message = $"Client {client.Id} - {client.Name} - {client.Email} deleted";
                logManager.AddLog(message, methodName, "Ok");

                return client;
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
