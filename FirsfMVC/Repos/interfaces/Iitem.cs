using FirsfMVC.Models;

namespace FirsfMVC.Repos.interfaces
{
    public interface Iitem
    {
        public Task<IEnumerable<Item>> GetItems();
        public Task AddItem(Item item);
        public Task UpdateItem(Item item);
        public Task DeleteItem(Item item);
        public Task<Item?> SearchItemByName(Item item);
        public Task<Item?> SearchItemById(int id);
        public Task<Item?> GetById(int id);
        public Task<IEnumerable<Category?>> GetCategories();
    }
}
