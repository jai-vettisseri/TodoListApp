using System;
using System.Collections.Generic;
using System.Text;
using Application.Common.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.TodoService
{
    public record DeleteTodoItem : IRequest<Unit>
    {
        public DeleteTodoItem(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }

    public class DeleteTodoHandler : IRequestHandler<DeleteTodoItem, Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;
        public DeleteTodoHandler(IApplicationDbContext context, ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<Unit> Handle(DeleteTodoItem request, CancellationToken cancellationToken)
        {
            var entity = await _context.TodoItems
                .Where(x => x.Id == request.Id && x.CreatedById == _currentUser.UserId)
                .FirstOrDefaultAsync();

            if (entity == null)
            {
                throw new ManagedException("Id is not valid");
            }

            entity.Status = Domain.Enums.TodoStatus.Inactive;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
