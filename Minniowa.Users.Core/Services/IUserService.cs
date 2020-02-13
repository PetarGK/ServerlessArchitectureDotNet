using Minniowa.Users.Core.Entities;
using System.Threading.Tasks;

namespace Minniowa.Users.Core.Services
{
    public interface IUserService
    {
        Task<int> Create(User user);
    }
}
