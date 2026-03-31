using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.Entities;

namespace TaskManager.application.Users.commands
{

    public class loginCommand :IRequest<loginResponse>
    {
        public string? username { get; set; }
        public string? password { get; set; }

        [EmailAddress]
        public string? email { get; set; }


    }

    public class loginResponse
    {
        public UserItem user { get; set; }

        public string token { get; set; }

        public string statuse {  get; set; }

        public string message { get; set; }

    }
}
