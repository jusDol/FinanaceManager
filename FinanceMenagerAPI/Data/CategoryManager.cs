using FinanceMenagerAPI.Data;
using FinanceMenagerAPI.Models;

namespace FinanceMenagerAPI.Controllers
{

    public class CategoryManager
    {
        private readonly FinanceContext _context;
        private List<Category>? _categories;

        // Konstruktor przyjmuje wstrzyknięty kontekst bazy danych
        public CategoryManager(FinanceContext context)
        {
            _context = context;
        }

        // Pobieranie kategorii z pamięci (lub z bazy danych, jeśli brak w pamięci)
        public List<Category> GetCategories()
        {
            if (_categories == null || !_categories.Any())
            {
                LoadCategoriesFromDatabase();
            }
            return _categories!;
        }

        // Odświeżanie danych w pamięci z bazy danych
        public void ReloadCategories()
        {
            LoadCategoriesFromDatabase();
        }

        // Prywatna metoda ładująca dane z bazy
        private void LoadCategoriesFromDatabase()
        {
            _categories = _context.Categories.ToList();
        }
    }
}
