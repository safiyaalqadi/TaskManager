using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using TaskManager.application.Interfaces;
using TaskManager.application.Users.commands;
using TaskManager.Domain.Entities;

namespace TaskManager.application.Users.handlers
{
    public class loginHandler : IRequestHandler<loginCommand, loginResponse>
    {
        private readonly IDBContext _context;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly UserManager<UserItem> _userManager;

        public loginHandler(IDBContext context, IMapper mapper, ITokenService token, UserManager<UserItem> userManager)
        {
            _context = context;
            _mapper = mapper;
            _tokenService = token;
            _userManager = userManager;

        }
        public async Task<loginResponse> Handle(loginCommand request, CancellationToken cancellationToken)
        {
            var loginResponse = new loginResponse() ;
            var res = new UserItem();



            /*  if (request.username == null)
              {
                  res = await _context.Users.Where(u => u.PasswordHash == request.password).Where(u=>(request.email != null && u.Email==request.email) 
                  || u.UserName == request.username).SingleOrDefaultAsync();

              }
              else if (request.email == null)
              {
                  res = await _context.Users.Where(u => u.PasswordHash == request.password).Where(u => u.UserName == request.username).SingleOrDefaultAsync();


              }*/

            UserItem user = null;

            if (!string.IsNullOrEmpty(request.username))
               
            {
                
                user = await _userManager.FindByNameAsync(request.username);
            }
            else if (!string.IsNullOrEmpty(request.email))
            {
                user = await _userManager.FindByEmailAsync(request.email);
            }
            var passwordResult = await _userManager.CheckPasswordAsync(user, request.password);
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.password))
            {
                return new loginResponse
                {
                    statuse = "failed",
                    message = "Invalid username/email or password"
                };
            }



            if (res != null)
            {
                List<string> roles = new List<string>();
               
                roles = (List<string> )await _userManager.GetRolesAsync(res);
                roles.Add(user.role);
                Console.WriteLine("this is the user name bassed : ", res.UserName);
                var token = _tokenService.CreateToken(user.UserName,user.PasswordHash, user.Id,roles);

                loginResponse.user = res;
                loginResponse.user.role = roles[0];
                loginResponse.token = token;
                loginResponse.statuse = "success";
                loginResponse.message = $"{res.Name} logging in successfully";

            }
            if (res == null)
            {
                var exisPassword = _context.Users.Where(u => u.PasswordHash == request.password);
                var exisUserName = _context.Users.Where(u => u.UserName == request.username);
                var exisEmail = _context.Users.Where(u => u.Email == request.email);
                if(exisPassword != null || exisUserName!=null || exisEmail!=null)
                {
                    loginResponse.message = "the Username/Email Or Password u insert was wrong";
                    loginResponse.statuse="success";
                }




            }
            




            return loginResponse;
        }
    }
}
