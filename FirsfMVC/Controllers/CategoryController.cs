
using FirsfMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirsfMVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _db;

        public CategoryController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.Categories.ToList());
        }

        public IActionResult New()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult New(Category category)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Add(category);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }


        public IActionResult Edit(int id)
        {
            if(id == 0)
            {
                return NotFound();
            }
            var category = _db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(category);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Edit");
        }

        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var category = _db.Categories.Find(id);
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Category category)
        {
            var categoryInDb = _db.Categories.Include(c => c.Items).FirstOrDefault(c => c.id == category.id);

            if (categoryInDb == null)
            {
                return NotFound();
            }

            // حذف العناصر المرتبطة
            if (categoryInDb.Items!.Any())
            {
                _db.Items.RemoveRange(categoryInDb.Items!);
            }

            _db.Categories.Remove(categoryInDb);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
