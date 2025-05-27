using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.Data;
using eCommerce.Models;
using eCommerce.Models.Resources;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eCommerce.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, 
            ApplicationDbContext context, 
            IWebHostEnvironment env)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _env = env;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Display(Name = "Profile Picture")]
            public IFormFile ProfilePicture { get; set; }
            public UserProfile UserProfile { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var userId = _userManager.GetUserId(User);
            var userProfile = _context.UserProfile.FirstOrDefault(up => up.UserId.ToString() == userId);

            Username = userName;

            Input = new InputModel
            {
                UserProfile = userProfile
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var isNewProfile = false;

            var userProfile = _context.UserProfile.FirstOrDefault(up => up.UserId.ToString() == user.Id);

            if (userProfile == null)
            {
                userProfile = new UserProfile();
                isNewProfile = true;
            }
            
            userProfile.UserId = Guid.Parse(user.Id);
            if (int.TryParse(Input.UserProfile.City, out int cityInt))
            {
                var cityEnum = (CitiesEnum)cityInt;
                var city = EnumHelpers.GetEnumDisplayName(cityEnum);
                userProfile.City = city;
            }
            else
            {
                userProfile.City = null;
            }
            userProfile.Name = Input.UserProfile.Name;
            userProfile.PhoneNumber = Input.UserProfile.PhoneNumber;
            userProfile.Bio = Input.UserProfile.Bio;
            userProfile.Email = Input.UserProfile.Email;

            // profile pic
            if (Input.ProfilePicture != null)
            {
                var fileName = UploadedFile(Input.ProfilePicture);
                userProfile.ProfilePic = "~/images/" + fileName;
            }

            if (isNewProfile)
                _context.Add(userProfile);

            _context.SaveChanges();

            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }

        private string UploadedFile(IFormFile file)
        {
            if (file != null)
            {
                var uploadsFolder = Path.Combine(_env.WebRootPath, "images");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                return uniqueFileName;
            }
            return string.Empty;
        }
    }
}
