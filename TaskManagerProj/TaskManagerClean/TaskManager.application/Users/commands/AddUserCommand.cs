using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.application.Tasks.Commands;

namespace TaskManager.application.Users.commands
{
    public class AddUserCommand : IRequest<addUseerResponse>
    {
        
        public string? Name { get; set; } = string.Empty;

        public string? firstname { get; set; } = string.Empty;
        public string? lastname { get; set; } = string.Empty;

        public string? Email { get; set; } = string.Empty;

        public string username { get; set; } = string.Empty;
        public string? password { get; set; } = string.Empty;

        public string? image { get; set; } = string.Empty;

        public string? role { get; set; } = string.Empty;

        public string? gender { get; set; } = string.Empty;

        public DateTime? DateOfBirth { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? Updated { get; set; } = DateTime.Now;
    }

    public class addUseerResponse
    {
        public string UserId { get; set; }
        public string Message { get; set; }

        public bool  Success { get; set;}


    }
}
