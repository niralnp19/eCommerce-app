using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.Data;
using eCommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace eCommerce.Areas.Identity.Pages.Account.Manage
{
    public class FollowersDataModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public IList<FollowerDetails> FollowerList { get; set; }
        public FollowersDataModel(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> OnGet()
        {
            var user = _userManager.GetUserId(User);
            var list = _context.Followers.Where(f => f.SellerUserId.ToString() == user).ToList();
            var followerList = new List<FollowerDetails>();
            foreach (var follower in list)
            {
                var followerDetails = new FollowerDetails();
                followerDetails.Follower = follower;
                followerDetails.UserProfile = _context.UserProfile.FirstOrDefault(up => up.UserId == follower.UserId);
                followerDetails.Email = followerDetails.UserProfile?.Email;
                if (string.IsNullOrEmpty(followerDetails.Email))
                {
                    var followerAcc = await _userManager.FindByIdAsync(follower.UserId.ToString());
                    if (followerAcc != null)
                    {
                        followerDetails.Email = followerAcc.Email;
                    }
                }
                followerList.Add(followerDetails);

            }
            this.FollowerList = followerList;
            return Page();
        }

        public class FollowerDetails {
            public Follower Follower { get; set; }
            public UserProfile UserProfile { get; set; }
            public string Email { get; set; }
        }
    }
}