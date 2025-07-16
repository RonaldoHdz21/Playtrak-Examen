using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ui.Models
{
    public class Client
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public long Telephone { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Column(TypeName = "date")]
        public DateTime Birthdate { get; set; }

        public int Type { get; set; }

        public bool Active { get; set; }

    }
}
