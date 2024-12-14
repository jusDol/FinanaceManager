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
        [Fact]
        public async Task GetCategories_ReturnsOkResult_WithListOfCategories()
        {
            // Arrange
            var mockCategoryManager = new Mock<ICategoryRepository>();

            var categories = new List<Category>
            {
                new Category { Id = 1, Name = "Technology" },
                new Category { Id = 2, Name = "Finance" }
            };

            mockCategoryManager.Setup(manager => manager.GetCategoriesAsync()).ReturnsAsync(categories);
            var controller = new CategoriesController(mockCategoryManager.Object);

            // Act
            var result = await controller.GetCategories();  

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result); 
            var returnedCategories = Assert.IsAssignableFrom<List<Category>>(okResult.Value);  
            Assert.Equal(categories.Count, returnedCategories.Count); 
        }

        [Fact]
        public async Task GetCategories_ReturnsEmptyList_WhenNoCategories()
        {
            // Arrange
            var mockCategoryManager = new Mock<ICategoryRepository>();

            mockCategoryManager.Setup(manager => manager.GetCategoriesAsync()).ReturnsAsync(new List<Category>());

            var controller = new CategoriesController(mockCategoryManager.Object);

            // Act
            var result = await controller.GetCategories();  

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result); 
            var returnedCategories = Assert.IsAssignableFrom<List<Category>>(okResult.Value); 
            Assert.Empty(returnedCategories);  
        }
    }
}
