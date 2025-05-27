using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Models
{
    public class UserProfile
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }
        [EmailAddress]
        [Display(Name = "Contact Email Address")]
        public string Email { get; set; }
        public string City { get; set; }
        public string ProfilePic { get; set; }
    }
}
