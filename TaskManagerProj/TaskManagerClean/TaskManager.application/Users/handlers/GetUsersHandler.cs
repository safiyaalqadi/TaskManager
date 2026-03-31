using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.application.DTOs;
using TaskManager.application.Interfaces;
using TaskManager.application.Tasks.queries;
using TaskManager.application.Users.queries;
using TaskManager.Domain.Entities;
using System.Linq.Expressions;

namespace TaskManager.application.Users.handlers
{
    public class GetUsersHandler : IRequestHandler<GetUsersQuery, GetUsersResponse>
    {
        private readonly IDBContext _context;
        private readonly IMapper _mapper;
        public GetUsersHandler(IDBContext context,IMapper mapper) {
           _context = context;
            _mapper = mapper;
        }

        public async Task<GetUsersResponse> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var res  = new GetUsersResponse();

            Expression<Func<UserItem, bool>> IdCndition = t => true;

            res.UsersCount = _context.Users.Where(IdCndition).Count();

            res.Users = await _mapper.ProjectTo<UserDto>(_context.Users.Skip((request.pageNumber - 1) * request.pageSize)
                .Take(request.pageSize))
                        .ToListAsync();

          


            return res;
        }
    }
}
