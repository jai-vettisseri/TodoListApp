using Application.Common.Models;
using Domain.Entities;
using Domain.Enums;

namespace Application.UnitTests.Todo
{
    public class MappingTests
    {
        [Fact]
        public void Should_Map_TodoItem_To_TodoDto_Correctly()
        {
            // Arrange
            var item = new TodoItem
            {
                Id = Guid.NewGuid(),
                Title = "Test Todo",
                Description = "Test Desc",
                DueDate = new DateTime(2026, 1, 1),
                Status = TodoStatus.Completed,
                CreatedDate = DateTime.Now,
                CreatedById = Guid.NewGuid()
            };

            // Act
            var dto = new TodoDto(item);

            // Assert
            Assert.Equal(item.Id, dto.Id);
            Assert.Equal(item.Title, dto.Title);
            Assert.Equal(item.Description, dto.Description);
            Assert.Equal(item.DueDate, dto.DueDate);
            Assert.Equal(item.Status.GetDisplayName(), dto.Status);
        }
    }
}