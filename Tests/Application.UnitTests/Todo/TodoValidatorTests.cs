using Application.Common.Models;
using Application.TodoService;
using FluentValidation.TestHelper;

namespace Application.UnitTests.Todo
{
    public class TodoValidatorTests
    {
        private readonly BaseTodoValidator<BaseTodo> _validator;
        public TodoValidatorTests()
        {
            _validator = new BaseTodoValidator<BaseTodo>();
        }

        [Fact]
        public void Should_Have_Error_When_Title_Is_Empty()
        {
            var model = new BaseTodo
            {
                Title = "",
                DueDate = DateTime.UtcNow.AddDays(1)
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Title);
        }

        [Fact]
        public void Should_Have_Error_When_Title_Exceeds_60_Chars()
        {
            var model = new BaseTodo
            {
                Title = new string('A', 61),
                DueDate = DateTime.UtcNow.AddDays(1)
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Title);
        }

        [Fact]
        public void Should_Have_Error_When_DueDate_Is_In_Past()
        {
            var model = new BaseTodo
            {
                Title = "Valid Title",
                DueDate = DateTime.UtcNow.AddDays(-1)
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.DueDate)
                  .WithErrorMessage("Due date cannot be in the past.");
        }

        [Fact]
        public void Should_Pass_When_Model_Is_Valid()
        {
            var model = new BaseTodo
            {
                Title = "Valid Title",
                DueDate = DateTime.UtcNow.AddDays(2)
            };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
