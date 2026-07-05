using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class TodoItem
    {
        public required Guid Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public required DateTime DueDate { get; set; }
        public TodoStatus Status { get; set; } = TodoStatus.Pending;
        public required Guid CreatedById { get; set; }
        public required DateTime CreatedDate { get; set; }

    }
}
