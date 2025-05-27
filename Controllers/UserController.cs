using eCommerce.Data;
using eCommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        public UserController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: UserController
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Chat()
        {
            return View();
        }

        // GET: UserController/Details/5
        public ActionResult Details(Guid id)
        {
            var viewModel = new SellerDetailsViewModel();
            viewModel.UserProfile = _context.UserProfile.FirstOrDefault(up => up.UserId == id) ?? new UserProfile();
            viewModel.Ads = _context.Ad.Include(ad => ad.AdImages).Where(a => a.UserId == id).ToList();
            var userId = _userManager.GetUserId(User);
            viewModel.ShowFollowButton = !string.IsNullOrEmpty(userId) && userId != viewModel.UserProfile.UserId.ToString();
            viewModel.IsFollowing = viewModel.ShowFollowButton && _context.Followers.Any(f => f.UserId.ToString() == userId && f.SellerUserId == viewModel.UserProfile.UserId);
            var reviews = _context.Reviews.Where(r => r.SellerUserId == id).ToList();
            if (reviews.Any())
            {
                var averageScore = reviews.Average(r => r.Score);
                viewModel.ShowReviewScore = true;
                viewModel.ReviewScore = Math.Round(averageScore * 2, MidpointRounding.AwayFromZero) / 2;
            }

            return View(viewModel);
        }

        public async Task<ActionResult> ToggleFollow(Guid id)
        {
            var userId = _userManager.GetUserId(User);
            if (userId != id.ToString())
            {
                var seller = await _userManager.FindByIdAsync(id.ToString());
                if (seller != null)
                {
                    var follower = _context.Followers.FirstOrDefault(f => f.UserId.ToString() == userId && f.SellerUserId.ToString() == seller.Id);
                    if (follower != null)
                    {
                        _context.Remove(follower);
                    }
                    else
                    {
                        var newFollower = new Follower();
                        newFollower.UserId = Guid.Parse(userId);
                        newFollower.SellerUserId = Guid.Parse(seller.Id);
                        newFollower.DateCreated = DateTime.UtcNow;
                        _context.Add(newFollower);
                    }
                    _context.SaveChanges();
                }
            }
            return RedirectToAction("Details", new { id });
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Reviews(Guid id)
        {
            var viewModel = new ReviewsViewModel();
            viewModel.UserProfile = _context.UserProfile.FirstOrDefault(up => up.UserId == id);
            viewModel.SellerUserId = id;
            viewModel.Reviews = _context.Reviews.Where(r => r.SellerUserId == id).ToList();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Reviews(Guid id, IFormCollection collection)
        {
            if (int.TryParse(collection["stars"], out int score))
            {
                var newReview = new Review();
                newReview.Score = score;
                newReview.Text = collection["text"];
                newReview.SellerUserId = id;
                newReview.UserId = Guid.Parse(_userManager.GetUserId(User));
                newReview.DateCreated = DateTime.UtcNow;

                var userProfile = _context.UserProfile.FirstOrDefault(up => up.UserId == newReview.UserId);
                if (userProfile != null)
                    newReview.DisplayName = userProfile.Name;
                else
                    newReview.DisplayName = "Anonymous";

                _context.Add(newReview);
                _context.SaveChanges();
            }

            return RedirectToAction("Reviews", new { id });
        }
    }
}
