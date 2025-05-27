using eCommerce.Models.Resources;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Models
{
    public class AdDetailsViewModel
    {
        public Ad Ad { get; set; }

        public IFormFile Picture { get; set; }

        public string AuthorName { get; set; }
        public bool CanDelete { get; set; }
        public bool CanEdit { get; set; }
    }
}
