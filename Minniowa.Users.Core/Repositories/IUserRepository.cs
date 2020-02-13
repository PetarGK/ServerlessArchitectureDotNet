using Minniowa.Users.Core.Entities;
using System.Threading.Tasks;

namespace Minniowa.Users.Core.Repositories
{
    public interface IUserRepository
    {
        Task<int> Add(User user);
    }
}
