using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ui.Models;
using ui.Data;

namespace ui.Services
{
    public class ClientService
    {
        public async Task<List<Client>> GetClients()
        {
            DataConnection data = new DataConnection();
            try
            {
                string json = await data.GetAsync("client", "GetClients");

                if (json.Length == 0)
                    return new List<Client>();

                Response responseApi = JsonConvert.DeserializeObject<Response>(json);
                List<Client> clients = JsonConvert.DeserializeObject<List<Client>>(responseApi.Data);

                return clients;
            }
            catch(Exception ex) 
            {
                return new List<Client>();
            }
        }

        public async Task<Response> GetClientById(int id)
        {
            DataConnection data = new DataConnection();
            try
            {
                string json = await data.GetAsync("client", "GetClientById", $"id={id}");

                if (json.Length == 0)
                    return new Response()
                    {
                        Status = 500,
                        Message = "Cliente no encontrado."
                    };

                Response responseApi = JsonConvert.DeserializeObject<Response>(json);

                return responseApi;
            }
            catch (Exception ex)
            {
                return new Response()
                {
                    Status = 500,
                    Message = ex.Message
                };
            }
        }

        public async Task<Response> AddClients(Client client)
        {
            DataConnection data = new DataConnection();
            try
            {
                string body = JsonConvert.SerializeObject(client);
                string json = await data.PostAsync("client", "AddClient", body);

                Response responseApi = JsonConvert.DeserializeObject<Response>(json);

                return responseApi;
            }
            catch (Exception ex)
            {
                return new Response()
                {
                    Status = 500,
                    Message = ex.Message
                };
            }
        }
        public async Task<Response> UpdateClient(int id, Client client)
        {
            DataConnection data = new DataConnection();
            try
            {
                string body = JsonConvert.SerializeObject(client);
                string json = await data.PutAsync("client", "UpdateClient", body, $"id={id}");

                Response responseApi = JsonConvert.DeserializeObject<Response>(json);

                return responseApi;
            }
            catch (Exception ex)
            {
                return new Response()
                {
                    Status = 500,
                    Message = ex.Message
                };
            }
        }

        public async Task<Response> DeleteClient(int id)
        {
            DataConnection data = new DataConnection();
            try
            {
                string json = await data.DeleteAsync("client", "DeleteClient", $"id={id.ToString()}");

                Response responseApi = JsonConvert.DeserializeObject<Response>(json);

                return responseApi;
            }
            catch (Exception ex)
            {
                return new Response()
                {
                    Status = 500,
                    Message = ex.Message
                };
            }
        }
    }
}