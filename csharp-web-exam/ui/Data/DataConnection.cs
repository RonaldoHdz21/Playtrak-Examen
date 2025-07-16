
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System.Configuration;
using ui.Models;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ui.Data
{
    public class DataConnection
    {
        public async Task<string> GetAsync(string controller, string method, string parameters = "")
        {
            string apiUrl = ConfigurationManager.AppSettings["ApiURL"];
            Response responseApi = new Response();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new System.Uri(apiUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.GetAsync($"{controller}/{method}?{parameters}");

                    string json = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        
                        responseApi.Status = 200;
                        responseApi.Data = json;
                        return JsonConvert.SerializeObject(responseApi);
                    }
                    else
                    {
                        responseApi.Message = (string)JObject.Parse(json)["error"];
                        return JsonConvert.SerializeObject(responseApi);
                    }
                }
            }
            catch
            { 
                return string.Empty;
            }
        }

        public async Task<string> PostAsync(string controller, string method, string body, string parameters = "")
        {
            string apiUrl = ConfigurationManager.AppSettings["ApiURL"];
            Response responseApi = new Response()
            {
                Status = 500,
                Message = ""
            };
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new System.Uri(apiUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpContent content = new StringContent(body, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync($"{controller}/{method}?{parameters}", content);



                    string json = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {

                        responseApi.Status = 200;
                        responseApi.Data = json;
                        return JsonConvert.SerializeObject(responseApi);
                    }
                    else
                    {
                        responseApi.Message = (string)JObject.Parse(json)["error"];
                        return JsonConvert.SerializeObject(responseApi);
                    }
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        public async Task<string> PutAsync(string controller, string method, string body = null, string parameters = "")
        {
            string apiUrl = ConfigurationManager.AppSettings["ApiURL"];
            Response responseApi = new Response()
            {
                Status = 500,
                Message = ""
            };
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new System.Uri(apiUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpContent content = null;
                    if (body != null)
                        content = new StringContent(body, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PutAsync($"{controller}/{method}?{parameters}", content);

                    string json = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        
                        responseApi.Status = 200;
                        responseApi.Data = json;
                        return JsonConvert.SerializeObject(responseApi);
                    }
                    else
                    {
                        responseApi.Message = (string)JObject.Parse(json)["error"];
                        return JsonConvert.SerializeObject(responseApi);
                    }
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        public async Task<string> DeleteAsync(string controller, string method, string parameters = "")
        {
            string apiUrl = ConfigurationManager.AppSettings["ApiURL"];
            Response responseApi = new Response()
            {
                Status = 500,
                Message = ""
            };
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new System.Uri(apiUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.DeleteAsync($"{controller}/{method}?{parameters}");

                    string json = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {

                        responseApi.Status = 200;
                        responseApi.Data = json;
                        return JsonConvert.SerializeObject(responseApi);
                    }
                    else
                    {
                        responseApi.Message = (string)JObject.Parse(json)["error"];
                        return JsonConvert.SerializeObject(responseApi);
                    }
                }
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
