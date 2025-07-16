using System;
using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class BalanceHistory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ClientId { get; set; }

        [Required]
        public double OldAmount { get; set; }

        [Required]
        public double NewAmount { get; set; }

        [Required]
        public DateTime DateTime { get; set; }
    }
}
