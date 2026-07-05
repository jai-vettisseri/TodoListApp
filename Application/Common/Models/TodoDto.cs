using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Enums;

namespace Application.Common.Models
{
    public record BaseTodo
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }

    public record TodoDto : BaseTodo
    {
        public Guid Id { get; set; }

        public TodoDto(TodoItem item)
        {
            Id = item.Id;
            Title = item.Title;
            Description = item.Description;
            DueDate = item.DueDate;
            Status = item.Status.GetDisplayName();
        }

    }
}
