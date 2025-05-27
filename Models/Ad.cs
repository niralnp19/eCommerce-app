using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Models
{
    public class Ad
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public double Price { get; set; }
        public bool IsBuying { get; set; }
        public string Category { get; set; }
        public Guid UserId { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DateUpdated { get; set; }

        public List<AdImage> AdImages { get; set; }
    }
}
