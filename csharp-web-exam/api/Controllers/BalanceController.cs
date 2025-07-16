using api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using api.Services.Implements;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BalanceController : Controller
    {
        private readonly IBalanceService _balanceService;
        public BalanceController(IBalanceService balanceService)
        {
            _balanceService = balanceService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("GetBalanceById")]
        public async Task<IActionResult> GetBalanceById(int id)
        {
            try
            {
                Balance balance = await _balanceService.GetBalanceByIdAsync(id);

                return Ok(balance);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "No se pudo obtener el balance.",
                    error = ex.Message
                });
            }
        }

        
        [HttpPut]
        [Route("UpdateBalance")]
        public async Task<IActionResult> UpdateBalance(int id, double amount)
        {
            try
            {
                Balance balanceUpdated = await _balanceService.UpdateBalanceAsync(id, amount);

                return Ok(balanceUpdated);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "No se pudo actualizar el balance.",
                    error = ex.Message
                });
            }
        }

        [HttpDelete]
        [Route("DeleteBalance")]
        public async Task<IActionResult> DeleteBalance(int id)
        {
            try
            {
                Balance balanceDeleted = await _balanceService.DeleteBalanceAsync(id);

                return Ok(balanceDeleted);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "No se pudo eliminar el balance.",
                    error = ex.Message
                });
            }
        }
    }
}
