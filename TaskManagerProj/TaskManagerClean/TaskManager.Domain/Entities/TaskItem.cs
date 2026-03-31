using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Domain.Entities
{
    public class TaskItem
    {

        public int Id { get; set; } = 0;

        public string? Title { get; set; } = string.Empty;

        public string? Description { get; set; } = string.Empty;

        public bool? IsCompleted { get; set; } = false;
        public bool? IsDeleted { get; set; } = false;

        public string? statuse { get; set; } = string.Empty;

        public string? userId { get; set; } 

        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; } = DateTime.Now;

        public UserItem user { get; set; }
    }
}
