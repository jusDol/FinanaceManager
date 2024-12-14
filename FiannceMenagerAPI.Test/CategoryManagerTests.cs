using FinanceMenagerAPI.Controllers;
using FinanceMenagerAPI.Data;
using FinanceMenagerAPI.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace FiannceMenagerAPI.Test
{
    public class CategoriesControllerTests
    {
        private readonly FinanceContext _context;
        private readonly CategoryManager _categoryManager;
        private readonly CategoriesController _controller;

        public CategoriesControllerTests()
        {
            var options = new DbContextOptionsBuilder<FinanceContext>()
                .UseInMemoryDatabase(databaseName: "FinanceTestDb")
                .Options;
            _context = new FinanceContext(options);

            _context.Categories.AddRange(
                new Category { Id = 1, Name = "Category 1" },
                new Category { Id = 2, Name = "Category 2" }
            );
            _context.SaveChanges();
            _categoryManager = new CategoryManager(_context);
            _controller = new CategoriesController(_categoryManager);
        }

        [Fact]
        public async Task GetCategories_ShouldReturnOkResultWithCategories()
        {
            // Act
            var result = await _controller.GetCategories();

            // Assert
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            var returnCategories = okResult?.Value as List<Category>;
            Assert.NotNull(returnCategories);
            Assert.Equal(2, returnCategories.Count);
            Assert.Equal("Category 1", returnCategories[0].Name);
            Assert.Equal("Category 2", returnCategories[1].Name);
        }
    }
}
