using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.TodoService
{
    public record CreateTodoItem : BaseTodo, IRequest<Guid>
    {
    }

    public class CreateTodoHandler : IRequestHandler<CreateTodoItem, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public CreateTodoHandler(IApplicationDbContext context, ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<Guid> Handle(CreateTodoItem request, CancellationToken ct)
        {
            var entity = new TodoItem
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                DueDate = request.DueDate,
                CreatedById = _currentUser.UserId ?? Guid.Empty,
                CreatedDate = DateTime.UtcNow,
                Status = EnumExtensions.GetEnumFromDisplayName<Domain.Enums.TodoStatus>(request.Status.ToString()) ?? Domain.Enums.TodoStatus.Pending
            };

            _context.TodoItems.Add(entity);
            await _context.SaveChangesAsync(ct);
            return entity.Id;
        }
    }
}
