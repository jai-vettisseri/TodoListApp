using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Application.TodoService
{

    public record GetTodo : IRequest<TodoDto>
    {
        public GetTodo(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; init; }
    }
    
    public class GetTodoHandler : IRequestHandler<GetTodo, TodoDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;
        public GetTodoHandler(IApplicationDbContext context, ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<TodoDto> Handle(GetTodo todo, CancellationToken cancellationToken)
        {
            var record = await _context.TodoItems
                    .AsNoTracking()
                    .Where(x => x.Id == todo.Id && x.CreatedById == _currentUser.UserId)
                    .FirstOrDefaultAsync(cancellationToken);
            if (record == null)
            {
                throw new ManagedException($"Record with id {todo.Id} not found.");
            }

            return new TodoDto(record);
            
        }
    }

    public record GetTodoList(bool includeCompletedOrDisabled = false) : IRequest<List<TodoDto>>;
    public class GetTodoListHandler : IRequestHandler<GetTodoList, List<TodoDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public GetTodoListHandler(IApplicationDbContext context, ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<List<TodoDto>> Handle(GetTodoList todo, CancellationToken cancellationToken)
        {
            var query = _context.TodoItems
                    .AsNoTracking()
                    .Where(x => x.CreatedById == _currentUser.UserId);

            if (!todo.includeCompletedOrDisabled)
            {
                query = query.Where(x => x.Status != Domain.Enums.TodoStatus.Completed && x.Status != Domain.Enums.TodoStatus.Inactive);
            }

            return await query.Select(record => new TodoDto(record)).ToListAsync(cancellationToken);

        }
    }
}
