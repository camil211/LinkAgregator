using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkGen.Data;
using LinkGen.Models;
using LinkGen.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace LinkGen.Converters
{
    public class ViewModelConverters
    {
        private readonly ApplicationDbContext _context;

        public ViewModelConverters(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task SetRateButtonVisibility(string userId, List<UrlViewModel> urlsViewModel)
        {
            if (userId != null)
            {
                var currentUser = await _context.Users.SingleAsync(u => u.Id == userId);

                foreach (var urlViewModelItem in urlsViewModel)
                {
                    bool urlRatedByCurrentUser = await _context.Rate.AnyAsync(x => x.Url.UrlId == urlViewModelItem.UrlId && x.User.Id == userId);
                    bool urlAddedByCurrentUser = currentUser.Urls != null && currentUser.Urls.Any(u => u.UrlId == urlViewModelItem.UrlId);
                    
                    urlViewModelItem.AllowRate = !urlRatedByCurrentUser && !urlAddedByCurrentUser;
                }
            }
        }

        public static IEnumerable<UrlViewModel> UrlToUrlViewModel(List<Url> urls)
        {
            var viewModel = urls.Select(u => new UrlViewModel()
            {
                UrlId = u.UrlId,
                Title = u.Title,
                Desc = u.Desc,
                UrlLink = u.UrlLink,
                Rating = u.Rating,
                UserId = u.User?.Id
            });
            return viewModel;
        }
    }
}
