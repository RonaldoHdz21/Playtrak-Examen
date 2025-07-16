using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)] 
        public string Name { get; set; }

        [Required]
        public long Telephone { get; set; }

        [Required]
        [MaxLength(200)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime Birthdate { get; set; }

        [Required]
        [ForeignKey("typeClient")]
        public int Type {  get; set; }

        [Required]
        public bool Active { get; set; }

        [JsonIgnore]
        public TypeClient typeClient { get; set; } 
    }
}
