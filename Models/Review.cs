using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Models
{
    public class Review
    {
        public Guid Id { get; set; }
        public Guid SellerUserId { get; set; }
        public int Score { get; set; }
        public string Text { get; set; }
        public Guid UserId { get; set; }
        public DateTime DateCreated { get; set; }
        public string DisplayName { get; set; }
    }
}
