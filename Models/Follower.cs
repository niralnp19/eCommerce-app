using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Models
{
    public class Follower
    {
        public Guid UserId { get; set; }
        public Guid SellerUserId { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
