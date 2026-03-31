using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.application.DTOs;

namespace TaskManager.application.Users.queries
{
    public class getoneUserQuery : IRequest<GetOneUserResponse>
    {
        public string? Id { get; set; }

        public string? UserName { get; set; }


    }

    public class GetOneUserResponse
    {
        public UserDto User { get; set; }



    }

}

  
