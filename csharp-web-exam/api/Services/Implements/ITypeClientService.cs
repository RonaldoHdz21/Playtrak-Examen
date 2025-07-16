using api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace api.Services.Implements
{
    public interface ITypeClientService
    {
        Task<List<TypeClient>> GetTypeClientsAsync();
        Task<TypeClient> GetTypeClientByIdAsync(int id);
    }
}
