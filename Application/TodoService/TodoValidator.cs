using Application.Common.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.TodoService
{
    public class BaseTodoValidator<T> : AbstractValidator<T> where T : BaseTodo
    {
        public BaseTodoValidator()
        {
            RuleFor(v => v.Title)
                .MaximumLength(60)
                .NotEmpty();

            RuleFor(v => v.DueDate)
                .GreaterThanOrEqualTo(DateTime.Today.ToUniversalTime())
                .WithMessage("Due date cannot be in the past.");
        }
    }

    public class CreateTodoValidator : BaseTodoValidator<CreateTodoItem>
    {
        public CreateTodoValidator()
        {
        }
    }

    public class UpdateTodoValidator : BaseTodoValidator<UpdateTodoItem>
    {
        public UpdateTodoValidator()
        {
        }
    }
}
