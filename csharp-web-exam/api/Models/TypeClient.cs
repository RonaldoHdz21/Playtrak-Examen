using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class TypeClient
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

    }
}
