using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using NuGet.ContentModel;
using NuGet.Packaging.Signing;
using WebApplication1.DAL;
using WebApplication1.Models;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class BookController : Controller
    {
        private PustokDbContext _context;
        public BookController(PustokDbContext context)
        {
            _context = context;
        }

        public IActionResult Detail(int id)
        {
            var book = _context.Books
                .Include(x => x.Author)
                .Include(x => x.Genre)
                .Include(x => x.BookImages)
                .FirstOrDefault(x => x.Id == id);

            return PartialView("BookModalPartial", book);
        }

        public IActionResult AddToBasket(int id)
        {
            var basket = HttpContext.Request.Cookies["basket"];
            List<BasketItemViewModel> basketItems;

            if (basket == null)
                basketItems = new List<BasketItemViewModel>();
            else
                basketItems = JsonConvert.DeserializeObject<List<BasketItemViewModel>>(basket);

            var wantedBook = basketItems.FirstOrDefault(x => x.BookId == id);
            if (wantedBook != null)
                wantedBook.Count++;
            else
                basketItems.Add(new BasketItemViewModel { BookId = id, Count = 1 });

            HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basketItems));
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ShowBasket()
        {
            var basket = HttpContext.Request.Cookies["basket"];
            var basketItems = JsonConvert.DeserializeObject<List<BasketItemViewModel>>(basket);
            //HttpContext.Response.Cookies.Delete("basket");
            return Json(basketItems);
        }
    }
}
