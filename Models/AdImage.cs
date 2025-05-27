using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Models
{
    public class AdImage
    {
        public Guid Id { get; set; }
        public Guid AdId { get; set; }
        public string ImagePath { get; set; }
        public Guid UserId { get; set; }
        public DateTime DateCreated { get; set; }

        [Required]
        [ForeignKey("AdId")]
        public Ad Ad { get; set; }
    }
}
