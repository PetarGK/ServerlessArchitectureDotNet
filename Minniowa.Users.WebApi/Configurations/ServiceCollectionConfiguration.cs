using Microsoft.Extensions.DependencyInjection;
using Minniowa.Users.Application.Users;
using Minniowa.Users.Core.Repositories;
using Minniowa.Users.Core.Services;
using Minniowa.Users.Infrastructure.ExternalServices;
using Minniowa.Users.Infrastructure.Repositories;
using Minniowa.Users.Services;
using Minniowa.Users.Services.ExternalServices;

namespace Minniowa.Users.WebApi.Configurations
{
    public static class ServiceCollectionConfiguration
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddTransient<IUserAppService, UserAppService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ICognitoService, CognitoService>();
        }
    }
}
