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
    public class signupCommand :IRequest<signupResponse>
    {


       public string? username {  get; set; }

        public string? password { get; set; } // assuming hte front make the validation to confirm bassword and the strong level s

        [EmailAddress]
        public string? email { get; set; }

        public string? firstname { get; set; }
        public string? lastname { get; set; }

        public DateTime? dateOfBirth { get; set; }

        public string? role { get; set; }

        public string? gender { get; set; }

        public string?  image { get; set; }


    }

    public class signupResponse
    {
      public  UserItem user { get; set; }

      public string Token { get; set; }

      public string refreshToken { get; set; }  
        
      public string message { get; set; }
      public string statuse { get; set; }


    }
}
