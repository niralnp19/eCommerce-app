using eCommerce.Data;
using eCommerce.Models;
using eCommerce.Models.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Controllers
{
    public class AdController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;
        public AdController(UserManager<ApplicationUser> userManager, ApplicationDbContext context, IWebHostEnvironment env)
        {
            _userManager = userManager;
            _context = context;
            _env = env;
        }

        // GET: AdController
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Map()
        {
            return View();
        }

        // GET: AdController/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var viewModel = new AdDetailsViewModel();
            viewModel.Ad = _context.Ad.Include(ad => ad.AdImages).FirstOrDefault(ad => ad.Id == id);
            if (viewModel.Ad != null)
            {
                var user = await _userManager.GetUserAsync(User);
                var author = await _userManager.FindByIdAsync(viewModel.Ad.UserId.ToString());
                viewModel.AuthorName = author.UserName;
                viewModel.CanDelete = user != null && (viewModel.Ad.UserId.ToString() == user.Id || await _userManager.IsInRoleAsync(user, "Admin"));
                viewModel.CanEdit = user != null && (viewModel.Ad.UserId.ToString() == user.Id);
            }
            return View(viewModel);
        }

        // GET: AdController/Create
        [Authorize(Roles = "User")]
        public ActionResult Create()
        {
            var userId = _userManager.GetUserId(User);
            var userProfile = _context.UserProfile.FirstOrDefault(up => up.UserId.ToString() == userId);
            if (userProfile == null)
            {
                TempData["StatusMessage"] = "Please complete your profile before posting an ad.";
                return LocalRedirect("/Identity/Account/Manage");
            }
            var viewModel = new CreateAdViewModel();
            return View(viewModel);
        }

        // POST: AdController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateAdViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Ad
                    var newAd = model.NewAd;
                    var user = await _userManager.GetUserAsync(User);
                    if (int.TryParse(model.NewAd.City, out int cityInt))
                    {
                        var cityEnum = (CitiesEnum)cityInt;
                        var city = EnumHelpers.GetEnumDisplayName(cityEnum);
                        newAd.City = city;
                    }
                    else
                    {
                        newAd.City = null;
                    }
                    newAd.UserId = Guid.Parse(user.Id);
                    newAd.DateCreated = DateTime.UtcNow;
                    _context.Add(newAd);
                    _context.SaveChanges();

                    // Ad image
                    if (model.Picture != null)
                    {
                        var fileName = UploadedFile(model);
                        var adImage = new AdImage();
                        adImage.AdId = newAd.Id;
                        adImage.DateCreated = DateTime.UtcNow;
                        adImage.ImagePath = "~/images/" + fileName;
                        adImage.UserId = Guid.Parse(user.Id);
                        _context.Add(adImage);
                        _context.SaveChanges();
                    }

                    return RedirectToAction(nameof(Details), new { id = newAd.Id });
                }
                return View();
            }
            catch (Exception e)
            {
                var test = e;
                return View();
            }
        }

        private string UploadedFile(CreateAdViewModel model)
        {
            if (model.Picture != null)
            {
                var uploadsFolder = Path.Combine(_env.WebRootPath, "images");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Picture.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Picture.CopyTo(fileStream);
                }
                return uniqueFileName;
            }
            return string.Empty;
        }

        [Authorize(Roles = "User")]
        // GET: AdController/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var viewModel = new CreateAdViewModel();
            var ad = _context.Ad.Include(ad => ad.AdImages).FirstOrDefault(ad => ad.Id == id);
            var user = await _userManager.GetUserAsync(User);
            if (ad != null && ad.UserId.ToString() == user.Id)
            {
                viewModel.NewAd = ad;
                return View(viewModel);
            }
            Response.StatusCode = 403;
            return View();
        }

        // POST: AdController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public async Task<ActionResult> Edit(Guid id, CreateAdViewModel model)
        {
            try
            {
                var ad = _context.Ad.Include(ad => ad.AdImages).FirstOrDefault(ad => ad.Id == id);
                var adUpdate = model.NewAd;
                var user = await _userManager.GetUserAsync(User);
                if (ad.UserId == Guid.Parse(user.Id))
                {
                    ad.DateUpdated = DateTime.UtcNow;
                    ad.Title = adUpdate.Title;
                    ad.Description = adUpdate.Description;
                    if (int.TryParse(adUpdate.City, out int cityInt))
                    {
                        var cityEnum = (CitiesEnum)cityInt;
                        var city = EnumHelpers.GetEnumDisplayName(cityEnum);
                        ad.City = city;
                    }
                    else
                    {
                        ad.City = null;
                    }
                    ad.Category = adUpdate.Category;
                    ad.Price = adUpdate.Price;

                    // Ad image
                    if (model.Picture != null)
                    {
                        var fileName = UploadedFile(model);
                        var adImage = new AdImage();
                        adImage.AdId = ad.Id;
                        adImage.DateCreated = DateTime.UtcNow;
                        adImage.ImagePath = "~/images/" + fileName;
                        adImage.UserId = Guid.Parse(user.Id);
                        _context.RemoveRange(ad.AdImages);
                        _context.Add(adImage);
                        _context.SaveChanges();
                    }

                    _context.SaveChanges();
                    return RedirectToAction(nameof(Details), new { id = ad.Id });
                }
                else
                    return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User, Admin")]
        public async Task<ActionResult> Delete(Guid Id)
        {
            try
            {
                var ad = _context.Ad.Include(ad => ad.AdImages).FirstOrDefault(ad => ad.Id == Id);
                var user = await _userManager.GetUserAsync(User);
                if (ad != null && (ad.UserId.ToString() == user.Id || await _userManager.IsInRoleAsync(user, "Admin")))
                {
                    if (ad.AdImages.Any())
                        ad.AdImages.ForEach(image => {
                            _context.Remove(image);
                        });
                    _context.Remove(ad);
                    _context.SaveChanges();
                }
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }
    }
}
