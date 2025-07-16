using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using api.Models;
using api.Services.Implements;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeClientController : Controller
    {
        private readonly ITypeClientService _typeClientService;
        public TypeClientController(ITypeClientService typeClientService)
        {
            _typeClientService = typeClientService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("GetTypeClients")]
        public async Task<IActionResult> GetTypeClients()
        {
            try
            {
                List<TypeClient> typeClients = await _typeClientService.GetTypeClientsAsync();

                return Ok(typeClients);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "No se pudo obtener la lista de tipos clientes.",
                    error = ex.Message
                });
            }
        }

        [HttpGet]
        [Route("GetTypeClientById")]
        public async Task<IActionResult> GetTypeClientById(int id)
        {
            try
            {
                TypeClient typeClients = await _typeClientService.GetTypeClientByIdAsync(id);

                return Ok(typeClients);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "No se pudo obtener el tipo de cliente.",
                    error = ex.Message
                });
            }
        }
    }
}
