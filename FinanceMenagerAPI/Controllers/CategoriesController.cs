using FinanceMenagerAPI.Data;
using Microsoft.AspNetCore.Mvc;

namespace FinanceMenagerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryManager;

        public CategoriesController(ICategoryRepository categoryManager)
        {
            _categoryManager = categoryManager;
        }

        [HttpGet("get-categories")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryManager.GetCategoriesAsync();
            return Ok(categories);
        }
    }
}
