using Minniowa.Users.Core.Entities;
using Minniowa.Users.Services.ExternalServices;
using System.Threading.Tasks;

namespace Minniowa.Users.Infrastructure.ExternalServices
{
    public class CognitoService : ICognitoService
    {
        public Task CreateUser(User user)
        {
            return Task.CompletedTask;
        }
    }
}
