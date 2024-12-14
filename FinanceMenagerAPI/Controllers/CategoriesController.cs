using FinanceMenagerAPI.Data;
using Microsoft.AspNetCore.Mvc;

namespace FinanceMenagerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryManager _categoryManager;

        public CategoriesController(ICategoryManager categoryManager)
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
