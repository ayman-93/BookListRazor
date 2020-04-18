using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Controllers
{
    [Route("api/book")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _db;
        public BookController(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.Book.ToListAsync() });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var bookFromDb2 = await _db.Book.FirstOrDefaultAsync(u => u.Id == id);
            if(bookFromDb2 == null)
            {
                return Json(new { success= false, message= "Error While Deleting" });
            }
            _db.Book.Remove(bookFromDb2);
            _db.SaveChanges();
            return Json(new { success = true, message = "Delete Successful" });

            //var bookFomDb = _db.Book.Find(id);
            //if(bookFomDb == null)
            //{
            //    return NotFound();
            //}
            //_db.Book.Remove(bookFomDb);
            //_db.SaveChanges();
            //return RedirectToPage("/BookList/Index");
        }
    }
}