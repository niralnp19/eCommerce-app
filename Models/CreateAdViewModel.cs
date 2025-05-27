using eCommerce.Models.Resources;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Models
{
    public class CreateAdViewModel
    {
        public CitiesEnum City { get; set; }

        public Ad NewAd { get; set; }

        public IFormFile Picture { get; set; }
    }
}
