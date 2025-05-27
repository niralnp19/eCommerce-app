using eCommerce.Data;
using eCommerce.Models;
using eCommerce.Models.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index(string searchText, string category, string city)
        {
            var ads = _context.Ad.Include(ads => ads.AdImages).AsEnumerable();

            if (category == "Select a Category")
            {
                category = string.Empty;
            }
            if (!string.IsNullOrEmpty(searchText))
            {
                ads = ads.Where(a => a.Title.ToLower().Contains(searchText.ToLower()) || a.Description.ToLower().Contains(searchText.ToLower()));
            }
            if (!string.IsNullOrEmpty(category))
            {
                ads = ads.Where(a => a.Category == category);
            }
            if (!string.IsNullOrEmpty(city))
            {
                if (int.TryParse(city, out int cityInt))
                {
                    var cityEnum = (CitiesEnum)cityInt;
                    var cityName = EnumHelpers.GetEnumDisplayName(cityEnum);
                    city = cityName;
                    ads = ads.Where(a => a.City == city);
                }
                else
                {
                    city = string.Empty;
                }
            }
            ViewBag.searchText = searchText;
            ViewBag.category = category;
            ViewBag.city = city;
            return View(ads.ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
