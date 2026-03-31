using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.application.DTOs;
using TaskManager.application.Tasks.queries;

namespace TaskManager.application.Users.queries
{
    public class GetUsersQuery: IRequest<GetUsersResponse>
    {

       
        public string? userId { get; set; }

        public DateTime? CreatedDate { get; set; }


        public int pageSize { get; set; } = 10;

        public int pageNumber { get; set; } = 1;

    }

    public class GetUsersResponse
    {
        public List<UserDto> Users { get; set; }

        public int ListSize { get; set; }

        public int UsersCount { get; set; }



    }
}
