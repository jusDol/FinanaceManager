using FinanceMenagerAPI.Data;
using Microsoft.AspNetCore.Mvc;

namespace FinanceMenagerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryManager _categoryManager;

        // Wstrzykiwanie CategoryManager
        public CategoriesController(CategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        [HttpGet("get-categories")]
        public IActionResult GetCategories()
        {
            // Pobieranie kategorii za pomocą CategoryManager
            var categories = _categoryManager.GetCategories();
            return Ok(categories);
        }

        [HttpPost("reload")]
        public IActionResult ReloadCategories()
        {
            // Odświeżenie danych w pamięci
            _categoryManager.ReloadCategories();
            return Ok("Categories reloaded.");
        }
    }
}
