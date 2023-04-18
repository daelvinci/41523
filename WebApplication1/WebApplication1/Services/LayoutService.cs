using WebApplication1.DAL;
using WebApplication1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using NuGet.ContentModel;
using NuGet.Packaging.Signing;

using WebApplication1.ViewModels;

namespace WebApplication1.Services
{
    public class LayoutService
    {
        private readonly IHttpContextAccessor _accessor;

        private PustokDbContext _context { get; set; }
        public LayoutService(PustokDbContext context, IHttpContextAccessor accessor)
        {
            _context = context;
            _accessor = accessor;
        }
        public List<Genre> GetGenres()
        {
            return _context.Genres.ToList();
        }

        public Dictionary<string,string> GetSettings()
        {
            return _context.Settings.ToDictionary(x => x.Key, x => x.Value);
        }

       public List<BasketItemViewModel> GetBasket()
        { 
            var basket= _accessor.HttpContext.Request.Cookies["basket"];
            var  basketItems = JsonConvert.DeserializeObject<List<BasketItemViewModel>>(basket);
            return basketItems;
        }
       

    }
}
