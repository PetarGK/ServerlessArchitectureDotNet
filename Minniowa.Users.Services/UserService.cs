using Minniowa.Users.Core.Entities;
using Minniowa.Users.Core.Repositories;
using Minniowa.Users.Core.Services;
using Minniowa.Users.Services.Exceptions;
using Minniowa.Users.Services.ExternalServices;
using Minniowa.Users.Services.Validators;
using System.Linq;
using System.Threading.Tasks;

namespace Minniowa.Users.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICognitoService _cognitoService;

        public UserService(IUserRepository userRepository, ICognitoService cognitoService)
        {
            _userRepository = userRepository;
            _cognitoService = cognitoService;
        }

        public async Task<int> Create(User user)
        {
            var validationResult = new UserValidatorCollection().Validate(user);

            if (!validationResult.IsValid)
                throw new InvalidDomainInvariantsException(validationResult.Errors.ToList());

            int userId = await _userRepository.Add(user);

            await _cognitoService.CreateUser(user);

            return userId;
        }
    }
}
