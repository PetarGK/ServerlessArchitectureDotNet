using Minniowa.Users.Application.Users.Requests;
using Minniowa.Users.Core.Entities;
using Minniowa.Users.Core.Services;
using Minniowa.Users.Services.Exceptions;
using System;
using System.Threading.Tasks;

namespace Minniowa.Users.Application.Users
{
    public class UserAppService : IUserAppService
    {
        private readonly IUserService _userService;
        public UserAppService(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Result> CreateUser(CreateUserRequest request) 
        {
            try
            {
                var user = new User
                {
                    Name = request.Name,
                    Email = request.Email
                };

                int userId = await _userService.Create(user);

                return new SuccessResult<int>(userId);
            }
            catch(InvalidDomainInvariantsException e)
            {
                return new ErrorResult(e.Errors);
            }
            catch(Exception e)
            {
                // Log the message

                return new UnhandledExceptionResult();
            }
        }
    }
}
