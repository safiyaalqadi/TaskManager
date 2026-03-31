using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.application.DTOs;
using TaskManager.application.Interfaces;
using TaskManager.application.Tasks.Commands;
using TaskManager.application.Users.commands;
using TaskManager.Domain.Entities;

namespace TaskManager.application.Users.handlers
{
    public class addUserHandler : IRequestHandler<AddUserCommand, addUseerResponse>
    {
        private readonly IDBContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<UserItem> _userManager;
        public addUserHandler(IDBContext context, IMapper mapper, UserManager<UserItem> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;


        }
        public async Task<addUseerResponse> Handle(AddUserCommand command, CancellationToken cancellationToken)
        {


            var UserResponse = new addUseerResponse();

            if (command.username ==null)
            {
                UserResponse.UserId = "";

                UserResponse.Message = $"userName was not found  :{command.username} ";
                UserResponse.Success = false;
                return UserResponse;
            }


           
           

          /* i dont have the isdelete attr in the database 
           * if (command.Id != null && command.Id > 0 )
            {
                var user = _context.Users.FindAsync(command.Id);
                if (user != null)
                {

                    UserResponse.UserId = user.Result.Id;
                    await _context.SaveChangesAsync(cancellationToken);

                    UserResponse.Message = $"user with id :{user.Result.Id} was deleted ";
                    UserResponse.Success = true;

                }

                else
                {
                    UserResponse.Message = $"user with id :{command.Id} not found to delete ";
                    UserResponse.Success = false;
                }



            }
            else */
            if (command.username != null )
            {

                var user = await _userManager.FindByNameAsync(command.username);


                if (user != null)
                {
                    var res = _mapper.Map(command, user);

                    await _userManager.UpdateAsync(user);
                    await _context.SaveChangesAsync(cancellationToken);

                    user.Updated = DateTime.Now;

                  
                    UserResponse.Message = $"user with Name :{user.UserName} was updated";
                    UserResponse.Success = true;
                }
                else
                {
                    UserResponse.Message = $"user with Name :{command.username} was not found to update";
                    UserResponse.Success = false;
                }


            }

            else
            {

                var user = new UserItem();






                var res = _mapper.Map(command, user);


                var added = _context.Users.Add(res);
                await _context.SaveChangesAsync(cancellationToken);

                UserResponse.UserId = res.Id;
                UserResponse.Message = $"user with Name :{user.UserName} was added";
                UserResponse.Success = true;
            }




            return UserResponse;




        }
    }
}
