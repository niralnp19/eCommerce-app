using eCommerce.Models.Resources;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Models
{
    public class ReviewsViewModel
    {
        public List<Review> Reviews { get; set; }
        public Guid SellerUserId { get; set; }
        public UserProfile UserProfile { get; set; }
    }
}
