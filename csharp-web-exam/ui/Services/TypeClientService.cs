using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ui.Data;
using ui.Models;

namespace ui.Services
{
    public class TypeClientService
    {
        public async Task<List<TypeClient>> GetTypeClients()
        {
            DataConnection data = new DataConnection();
            try
            {
                string json = await data.GetAsync("typeClient", "GetTypeClients");

                if (json.Length == 0)
                    return new List<TypeClient>();

                Response responseApi = JsonConvert.DeserializeObject<Response>(json);
                List<TypeClient> typeClients = JsonConvert.DeserializeObject<List<TypeClient>>(responseApi.Data);

                return typeClients;
            }
            catch (Exception ex)
            {
                return new List<TypeClient>();
            }
        }
    }
}
