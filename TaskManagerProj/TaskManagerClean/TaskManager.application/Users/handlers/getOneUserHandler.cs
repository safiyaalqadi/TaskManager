using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskManager.application.DTOs;
using TaskManager.application.Interfaces;
using TaskManager.application.Tasks.queries;
using TaskManager.application.Users.queries;
using TaskManager.Domain.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TaskManager.application.Users.handlers
{
    public class getOneUserHandler : IRequestHandler<getoneUserQuery, GetOneUserResponse>
    {

        private readonly IDBContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<UserItem> _userManager;

        public getOneUserHandler(IDBContext context, IMapper mapper, UserManager<UserItem> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<GetOneUserResponse> Handle(getoneUserQuery request, CancellationToken cancellationToken)
        {
            var vm = new GetOneUserResponse();

            Expression<Func<UserItem, bool>> IdCndition = t => true;

              if (request.UserName != null)
            {
                IdCndition = (t => t.UserName == request.UserName);
                var filteredUsers = _context.Users
                 .Where(IdCndition);

                var user = await _userManager.FindByNameAsync(request.UserName);



                vm.User = await _mapper.ProjectTo<UserDto>(filteredUsers).SingleOrDefaultAsync(cancellationToken);
            }

            else if (request.Id !=null )
            {
                IdCndition = (t => t.Id == request.Id);
                var filteredUsers = _context.Users
           .Where(IdCndition);

                var user = await _userManager.FindByIdAsync(request.Id);



                vm.User = await _mapper.ProjectTo<UserDto>(filteredUsers).SingleOrDefaultAsync(cancellationToken);
            }

           






            return vm;

        }
    }
}
