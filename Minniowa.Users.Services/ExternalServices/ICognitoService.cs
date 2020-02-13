using Minniowa.Users.Core.Entities;
using System.Threading.Tasks;

namespace Minniowa.Users.Services.ExternalServices
{
    public interface ICognitoService
    {
        Task CreateUser(User user);
    }
}
