using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.application.DTOs
{
    public class UserDto
    {

        public string Id { get; set; }
        public string? Name { get; set; } = string.Empty;

        public string? firstName { get; set; } = string.Empty;
        public string? lastName { get; set; } = string.Empty;

        public string? Email { get; set; } = string.Empty;

        public string? UserName { get; set; } = string.Empty;
        public string? Password { get; set; } = string.Empty;

        public string? image { get; set; } = string.Empty;

        public string? role { get; set; } = string.Empty;

        public string? gender { get; set; } = string.Empty;

        public DateTime? DateOfBirth { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? Updated { get; set; } = DateTime.Now;

    }
}
