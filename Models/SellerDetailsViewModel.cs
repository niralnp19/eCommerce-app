using eCommerce.Models.Resources;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Models
{
    public class SellerDetailsViewModel
    {
        public UserProfile UserProfile { get; set; }
        public List<Ad> Ads { get; set; }
        public bool ShowFollowButton { get; set; }
        public bool IsFollowing { get; set; }
        public bool ShowReviewScore { get; set; }
        public double ReviewScore { get; set; }
    }
}
