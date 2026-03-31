
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.application.DTOs
{
    public class TaskDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsCompleted { get; set; }

        public string? statuse { get; set; } = string.Empty;

        public bool IsDeleted { get; set; }

        public string UserId { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
