using api.Models;
using api.Services.Implements;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : Controller
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("GetClients")]
        public async Task<IActionResult> GetClients()
        {
            try
            {
                List<Client> clients = await _clientService.GetClientsAsync();

                return Ok(clients);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "No se pudo obtener la lista de clientes.",
                    error = ex.Message
                });
            }
        }

        [HttpGet]
        [Route("GetClientById")]
        public async Task<IActionResult> GetClientById(int id)
        {
            try
            {
                Client clients = await _clientService.GetClientByIdAsync(id);

                return Ok(clients);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "No se pudo obtener el cliente.",
                    error = ex.Message
                });
            }
        }

        [HttpPost]
        [Route("AddClient")]
        public async Task<IActionResult> AddClient([FromBody]Client client)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Client clientAdded = await _clientService.AddClientAsync(client);

                return Ok(clientAdded);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "No se pudo crear el cliente.",
                    error = ex.Message
                });
            }
        }

        [HttpPut]
        [Route("UpdateClient")]
        public async Task<IActionResult> UpdateClient(int id, Client clientMod)
        {
            try
            {
                Client clientUpdated = await _clientService.UpdateClientAsync(id, clientMod);

                return Ok(clientUpdated);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "No se pudo actualizar el cliente.",
                    error = ex.Message
                });
            }
        }

        [HttpDelete]
        [Route("DeleteClient")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            try
            {
                Client clientDeleted = await _clientService.DeleteClientAsync(id);

                return Ok(clientDeleted);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "No se pudo eliminar el cliente.",
                    error = ex.Message
                });
            }
        }
    }
}
