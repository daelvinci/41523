using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DAL;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class BookController:Controller
    {
        private PustokDbContext _context;
        public BookController(PustokDbContext context)
        {
            _context = context;  
        }
            
        public IActionResult Detail(int id)
        {
            var book = _context.Books
                .Include(x=>x.Author)
                .Include(x=>x.Genre)
                .Include(x => x.BookImages)
                .FirstOrDefault(x=>x.Id==id);

            return PartialView("BookModalPartial",book);
        }
    }
}
