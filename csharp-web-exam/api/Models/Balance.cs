using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    public class Balance
    {
        [Key]
        [Required]
        [ForeignKey("client")]
        public int ClientId { get; set; }

        [Required]
        public double BalanceAmount {  get; set; }

        [Required]
        public bool Active { get; set; }

        [JsonIgnore]
        public Client client { get; set; }
    }
}
