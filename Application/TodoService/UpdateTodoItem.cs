using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace Application.TodoService
{
    public record UpdateTodoItem : BaseTodo, IRequest<Unit>
    {
        public Guid Id { get; init; }
    }

    public class UpdateTodoHandler : IRequestHandler<UpdateTodoItem, Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public UpdateTodoHandler(IApplicationDbContext context, ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }
        public async Task<Unit> Handle(UpdateTodoItem request, CancellationToken ct)
        {
            var entity = await _context.TodoItems
                .Where(x => x.Id == request.Id && x.CreatedById == _currentUser.UserId)
                .FirstOrDefaultAsync();

            if (entity == null)
            {
                throw new ManagedException("Id is not valid");
            }

            entity.Title = request.Title;
            entity.Description = request.Description;
            entity.DueDate = request.DueDate;
            entity.Status = EnumExtensions.GetEnumFromDisplayName<Domain.Enums.TodoStatus>(request.Status.ToString()) ?? Domain.Enums.TodoStatus.Pending;

            await _context.SaveChangesAsync(ct);
            return Unit.Value;
        }
    }
}
