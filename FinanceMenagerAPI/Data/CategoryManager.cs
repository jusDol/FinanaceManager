using FinanceMenagerAPI.Data;
using FinanceMenagerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceMenagerAPI.Controllers
{
    public interface ICategoryManager
    {
        Task<List<Category>> GetCategories();
    }
    public class CategoryManager : ICategoryManager
    {
        private readonly FinanceContext _context;

        public CategoryManager(FinanceContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }
    }
}
