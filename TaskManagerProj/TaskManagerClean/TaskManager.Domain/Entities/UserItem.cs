using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

using Microsoft.EntityFrameworkCore;

namespace TaskManager.Domain.Entities
{
    public class UserItem : IdentityUser
    {
       // public int Id { get; set; }
        public string? Name { get; set; } = string.Empty;

        public string? firstname { get; set; } = string.Empty;
        public string? lastname { get; set; } = string.Empty;

      ///  public string? Email { get; set; } = string.Empty;

       // public string? username { get; set; } = string.Empty;
      //  public string? password { get; set; } = string.Empty;

        public string? image { get; set; } = string.Empty;

        public string? role { get; set; } = string.Empty;

        public string? gender { get; set; } = string.Empty;

        public DateTime? DateOfBirth { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? Updated { get; set; } = DateTime.Now;

        public ICollection<TaskItem> Tasks { get; set; }


    }
}
