using Minniowa.Users.Core.Entities;
using Minniowa.Users.Core.Repositories;
using System.Threading.Tasks;

namespace Minniowa.Users.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        public Task<int> Add(User user)
        {

            return Task.FromResult(1);
        }
    }
}
