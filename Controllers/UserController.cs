using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkGen.Data;
using LinkGen.Converters;
using LinkGen.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LinkGen.Models;

namespace LinkGen.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<UserApplication> _userManager;

        public UserController(ApplicationDbContext context, UserManager<UserApplication> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
      
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(HttpContext.User);      
            var urls = await _context.Urls.Where(u => u.User.Id == userId).OrderByDescending(x => x.Rating).ToListAsync();          
            
            IEnumerable<UrlViewModel> urlsViewModel = ViewModelConverters.UrlToUrlViewModel(urls);       

            return View(urlsViewModel);
        }

        public IActionResult CreateUrl()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUrl(UrlViewModel urlViewModel)
        {
            if (ModelState.IsValid)
            {
                Url url = new Url();
                url.UrlLink = urlViewModel.UrlLink;
                url.Title = urlViewModel.Title;
                url.Desc = urlViewModel.Desc;
                url.CreateDate = DateTime.UtcNow;                
                var userId = _userManager.GetUserId(HttpContext.User);                                                                             
                url.User = _context.Users.Single(u => u.Id == userId);                                
                _context.Urls.Add(url);

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}