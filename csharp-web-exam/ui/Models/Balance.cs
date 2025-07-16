using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ui.Models
{
    public class Balance
    {
       
        public int ClientId { get; set; }

        public double BalanceAmount {  get; set; }

        public bool Active { get; set; }
    }
}
