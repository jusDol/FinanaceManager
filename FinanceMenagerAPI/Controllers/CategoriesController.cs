using FinanceMenagerAPI.Data;
using Microsoft.AspNetCore.Mvc;

namespace FinanceMenagerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryManager _categoryManager;

        public CategoriesController(CategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        [HttpGet("get-categories")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryManager.GetCategories();
            return Ok(categories);
        }
    }
}
