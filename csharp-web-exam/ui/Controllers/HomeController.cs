
using System.Collections.Generic;
using System.Web.Mvc;
using ui.Models;
using System.Threading.Tasks;
using System.Linq;
using ui.Services;
using System;
using System.Net.Mail;
using Newtonsoft.Json;
using System.Web.Helpers;
using System.Xml.Linq;

namespace ui.Controllers
{
    public class HomeController : Controller
    {
        private ClientService clientService;
        private TypeClientService typeClientService;
        private BalanceService balanceService;
        public HomeController()
        {
            clientService = new ClientService();
            typeClientService = new TypeClientService();
            balanceService = new BalanceService();
        }
        public async Task<ActionResult> Index(int page = 1)
        {
            List<Client> clients = new List<Client>();
            int pageSize = 10;

            clients = await clientService.GetClients();

            int totalClients = clients.Count;
            int totalPages = (int)System.Math.Ceiling((double)totalClients / pageSize);

            var clientsPag = clients
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

            //Type clients
            ViewBag.TypeClients = await typeClientService.GetTypeClients();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View(clientsPag);
        }

        public async Task<ActionResult> GetClientById(int id)
        {
            Response response = new Response();
            response.Status = 500;
            try
            {
                Response responseApi = await clientService.GetClientById(id);

                if (responseApi.Status == 200)
                {
                    Client clientGetted = JsonConvert.DeserializeObject<Client>(responseApi.Data);

                    response.Data = JsonConvert.SerializeObject(clientGetted);
                    response.Message = "";
                    response.Status = 200;
                }
                else
                {
                    response.Message = responseApi.Message;
                }

                


                return Content(JsonConvert.SerializeObject(response));
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return Content(JsonConvert.SerializeObject(response));
            }
        }

        public async Task<ActionResult> AddClient(string name, string telephone, string email, DateTime birthdate, int typeClient)
        {
            Response response = new Response();
            response.Status = 500;
            try
            {
                //Type clients
                List<TypeClient> typeClients = await typeClientService.GetTypeClients();

                if (!telephone.All(char.IsDigit))
                    response.Message = "Error: Teléfono Inválido, solo se admiten numeros.";

                else if (!IsEmailValid(email))
                    response.Message = "Error: Correo inválido.";

                else if (typeClients.Where(a => a.Id == typeClient).Count() == 0)
                    response.Message = "Error: Tipo de cliente inválido.";

                else
                {
                    Client client = new Client()
                    {
                        Name = name,
                        Telephone = long.Parse(telephone),
                        Email = email,
                        Birthdate = birthdate,
                        Type = typeClient,
                        Active = true
                    };

                    Response responseApi = await clientService.AddClients(client);

                    if (responseApi.Status == 200) 
                    {
                        Client clientAdded = JsonConvert.DeserializeObject<Client>(responseApi.Data);

                        response.Data = JsonConvert.SerializeObject(clientAdded);
                        response.Message = "";
                        response.Status = 200;
                    }
                    else
                    {
                        response.Message = responseApi.Message;
                    }

                }
                

                return Content(JsonConvert.SerializeObject(response));
            }
            catch (Exception ex) 
            { 
                response.Message = ex.Message;
                return Content(JsonConvert.SerializeObject(response));
            }
        }

        public async Task<ActionResult> UpdateClient(int id, string name, string telephone, string email, DateTime birthdate, int typeClient)
        {
            Response response = new Response();
            response.Status = 500;
            try
            {

                //Type clients
                List<TypeClient> typeClients = await typeClientService.GetTypeClients();

                if (!telephone.All(char.IsDigit))
                    response.Message = "Error: Teléfono Inválido, solo se admiten numeros.";

                else if (!IsEmailValid(email))
                    response.Message = "Error: Correo inválido.";

                else if (typeClients.Where(a => a.Id == typeClient).Count() == 0)
                    response.Message = "Error: Tipo de cliente inválido.";

                else
                {
                    Client client = new Client()
                    {
                        Name = name,
                        Telephone = long.Parse(telephone),
                        Email = email,
                        Birthdate = birthdate,
                        Type = typeClient,
                        Active = true
                    };

                    Response responseApi = await clientService.UpdateClient(id, client);

                    if (responseApi.Status == 200)
                    {
                        Client clientUpdated = JsonConvert.DeserializeObject<Client>(responseApi.Data);

                        response.Data = JsonConvert.SerializeObject(clientUpdated);
                        response.Message = "";
                        response.Status = 200;
                    }
                    else
                    {
                        response.Message = responseApi.Message;
                    }

                }


                return Content(JsonConvert.SerializeObject(response));
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return Content(JsonConvert.SerializeObject(response));
            }
        }

        public async Task<ActionResult> DeleteClient(int id)
        {
            Response response = new Response();
            response.Status = 500;
            try
            {

                Response responseApi = await clientService.DeleteClient(id);

                if (responseApi.Status == 200)
                {
                    Client clientDeleted = JsonConvert.DeserializeObject<Client>(responseApi.Data);

                    response.Data = JsonConvert.SerializeObject(clientDeleted);
                    response.Message = "";
                    response.Status = 200;
                }
                else
                {
                    response.Message = responseApi.Message;
                }

                


                return Content(JsonConvert.SerializeObject(response));
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return Content(JsonConvert.SerializeObject(response));
            }
        }

        public async Task<ActionResult> GetBalanceById(int id)
        {
            Response response = new Response();
            response.Status = 500;
            try
            {
                Response responseApi = await balanceService.GetBalanceById(id);

                if (responseApi.Status == 200)
                {
                    Balance balanceGetted = JsonConvert.DeserializeObject<Balance>(responseApi.Data);

                    response.Data = JsonConvert.SerializeObject(balanceGetted);
                    response.Message = "";
                    response.Status = 200;
                }
                else
                {
                    response.Message = responseApi.Message;
                }




                return Content(JsonConvert.SerializeObject(response));
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return Content(JsonConvert.SerializeObject(response));
            }
        }

        public async Task<ActionResult> UpdateBalance(int id, double amount)
        {
            Response response = new Response();
            response.Status = 500;
            try
            {

                if (!amount.ToString().All(char.IsDigit))
                    response.Message = "Error: Monto inválido.";
                else
                {
                   

                    Response responseApi = await balanceService.UpdateBalance(id, amount);

                    if (responseApi.Status == 200)
                    {
                        Balance balanceUpdated = JsonConvert.DeserializeObject<Balance>(responseApi.Data);

                        response.Data = JsonConvert.SerializeObject(balanceUpdated);
                        response.Message = "";
                        response.Status = 200;
                    }
                    else
                    {
                        response.Message = responseApi.Message;
                    }

                }


                return Content(JsonConvert.SerializeObject(response));
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return Content(JsonConvert.SerializeObject(response));
            }
        }

        public static bool IsEmailValid(string email)
        {
            try
            {
                var direccion = new MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}