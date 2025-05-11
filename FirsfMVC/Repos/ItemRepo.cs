using FirsfMVC.Models;
using FirsfMVC.Repos.interfaces;
using Microsoft.EntityFrameworkCore;

namespace FirsfMVC.Repos
{
    public class ItemRepo : Iitem
    {
        private readonly AppDbContext _context;
        public ItemRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Item>> GetItems()
        {
            return await _context.Items.Include(i=>i.Category).ToListAsync();
        }
        
        public async Task AddItem(Item item)
        {
            await _context.Items.AddAsync(item);
            _context.SaveChanges();
            return;
        }
        public async Task UpdateItem(Item item)
        {
            var local =  _context.Items.Local.FirstOrDefault(i => i.id == item.id);

            if (local != null)
            {
                // فصل الكائن القديم
                _context.Entry(local).State = EntityState.Detached;
            }

            // تحديث الكائن الجديد
            _context.Items.Update(item);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteItem(Item item)
        {
           _context.Items.Remove(item);
            await _context.SaveChangesAsync();
        }
       
        public async Task<Item?> SearchItemByName(Item item)
        {
            var SearchedItem = await _context.Items.SingleOrDefaultAsync(g => g.name.ToUpper() == item.name.ToUpper());
            return SearchedItem;
        }
        public Task<Item?> SearchItemById(int id)
        {
            var SearchedItem = _context.Items.Include(i=>i.Category).SingleOrDefaultAsync(g => g.id == id);
            return SearchedItem;
        }

        public async Task<IEnumerable<Category?>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Item?> GetById(int id)
        {
           return await _context.Items.FirstOrDefaultAsync(i=>i.id == id);
        }
    }
}
