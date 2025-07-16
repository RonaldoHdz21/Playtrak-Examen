using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ui.Data;
using ui.Models;

namespace ui.Services
{
    public class BalanceService
    {
        public async Task<Response> GetBalanceById(int id)
        {
            DataConnection data = new DataConnection();
            try
            {
                string json = await data.GetAsync("balance", "GetBalanceById", $"id={id}");

                if (json.Length == 0)
                    return new Response()
                    {
                        Status = 500,
                        Message = "Saldo no encontrado."
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

        public async Task<Response> UpdateBalance(int id, double amount)
        {
            DataConnection data = new DataConnection();
            try
            {
                string json = await data.PutAsync("balance", "UpdateBalance", null, $"id={id}&amount={amount}");

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
