using Minniowa.Users.Application.Users.Requests;
using System.Threading.Tasks;

namespace Minniowa.Users.Application.Users
{
    public interface IUserAppService
    {
        Task<Result> CreateUser(CreateUserRequest request);
    }
}
