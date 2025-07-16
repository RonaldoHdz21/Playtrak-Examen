using api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace api.Services.Implements
{
    public interface IClientService
    {
        Task<List<Client>> GetClientsAsync();
        Task<Client> GetClientByIdAsync(int id);
        Task<Client> AddClientAsync(Client client);
        Task<Client> UpdateClientAsync(int id, Client clientMod);
        Task<Client> DeleteClientAsync(int id);
    }
}
