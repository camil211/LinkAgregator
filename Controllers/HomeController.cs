using LinkGen.Converters;
using LinkGen.Data;
using LinkGen.Models;
using LinkGen.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace LinkGen.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<UserApplication> _userManager;

        public HomeController(ApplicationDbContext context, UserManager<UserApplication> userManager)
        {
            _context = context;
            _userManager = userManager;            
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            IEnumerable<UrlViewModel> urlsViewModel = await GetMainPageData();

            return View(urlsViewModel);
        }

        private async Task<IEnumerable<UrlViewModel>> GetMainPageData()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var urls = await _context.Urls.Where(d => d.CreateDate >= DateTime.UtcNow.AddDays(-5)).OrderByDescending(x => x.Rating).ToListAsync();

            ViewModelConverters vievModelConverter = new ViewModelConverters(_context);
            List<UrlViewModel> urlsViewModel = ViewModelConverters.UrlToUrlViewModel(urls).ToList();
            await vievModelConverter.SetRateButtonVisibility(userId, urlsViewModel);

            return urlsViewModel;
        }

        [HttpPost]
        public async Task<IActionResult> RateClick(int id)
        {
            var url = _context.Urls.FirstOrDefault(x => x.UrlId == id);
            var userId = _userManager.GetUserId(HttpContext.User);                      
            Rate rate = _context.Rate.FirstOrDefault(x => x.Url.UrlId == id && x.User.Id == userId);        

            if (url != null && rate == null)
            {
                rate = new Rate();
                rate.User = _userManager.Users.Single(x => x.Id == userId);
                rate.Url = url;
                rate.RateDate = DateTime.UtcNow;

                url.Rating++;

                await _context.Rate.AddAsync(rate);
                await _context.SaveChangesAsync();
            }                                   
            
            IEnumerable<UrlViewModel> urlsViewModel = await GetMainPageData();

            return PartialView("_RankViewPartial", urlsViewModel);
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
