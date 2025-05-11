using FirsfMVC.Models;
using FirsfMVC.Repos.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FirsfMVC.Controllers
{
    [Authorize(Roles ="User")]
    public class ItemController : Controller
    {
        private readonly Iitem _item;
        private readonly IWebHostEnvironment _host;

        public ItemController(Iitem iitem,IWebHostEnvironment host)
        {
            _item = iitem;
            _host = host;
        }

        public async Task<IActionResult> Index()
        {
           var itemList = await _item.GetItems();
            return View(itemList);
        }
       
        public async Task<IActionResult> New()
        {
            ViewBag.Categories= await _item.GetCategories();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(Item item)
        {
            if(item.ClientFile!=null)
            {
                var upLoad = Path.Combine(_host.WebRootPath, "images");
                var fileName=item.ClientFile.FileName;
                var fullPath=Path.Combine(upLoad, fileName);
                item.ClientFile.CopyTo(new FileStream(fullPath,FileMode.Create));
                item.ImagePath = fileName;
            }
            var i = await _item.SearchItemByName(item);
            if (i != null)
            {
                ModelState.AddModelError("name", "This item already exists.");
                return View(item);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _item.AddItem(item);
            TempData["Success"] = "Item Has been Added Successfully";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Search(string searchString)
        {
            var items = await _item.GetItems(); 

            if (!string.IsNullOrEmpty(searchString))
            {
                items = items.Where(i => i.name.ToUpper().Contains(searchString.ToUpper())); 
            }

            return View("Search", items); 
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var item = await _item.SearchItemById(id);
            if (item == null)
            {
                return NotFound();
            }
            ViewBag.Categories = await _item.GetCategories();
            return View(item);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
           
            var item=await _item.SearchItemById(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Item item)
        {
            var existitems = await _item.GetById(item.id);
            item.ImagePath = existitems?.ImagePath;
            if (item.ClientFile != null)
            {
                var fileName = Path.GetFileName(item.ClientFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    item.ClientFile.CopyTo(stream);
                }

                item.ImagePath = fileName;
            }

            if (!ModelState.IsValid)
            {
                return View(item);
            }
            await _item.UpdateItem(item);
            TempData["Success"] = "Item Has been Edited Successfully";
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var item = await _item.SearchItemById(id);
            if (item == null)
            {
                return NotFound();
            }
            ViewBag.Categories = await _item.GetCategories();
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Item item)
        {

            await _item.DeleteItem(item);
            TempData["Success"] = "Item Has been Deleted Successfully";
            return RedirectToAction("Index");
        }

    }
}
