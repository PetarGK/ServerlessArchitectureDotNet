using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Minniowa.Users.Application.Users;
using Minniowa.Users.Application.Users.Requests;
using Minniowa.Users.Infrastructure.ExternalServices;
using Minniowa.Users.Infrastructure.Repositories;
using Minniowa.Users.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace Minniowa.Users.Lambda
{
    public class Functions
    {
        private readonly IUserAppService _userAppService;

        static Functions()
        {
            // Initialize logs, etc...
        }

        /// <summary>
        /// Default constructor that Lambda will invoke.
        /// </summary>
        public Functions() : this(new UserAppService(new UserService(new UserRepository(), new CognitoService())))
        { }

        /// <summary>
        /// Constructor used for testing.
        /// </summary>
        public Functions(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        public async Task<APIGatewayProxyResponse> CreateUser(APIGatewayProxyRequest proxyRequest, ILambdaContext context)
        {
            var request = JsonConvert.DeserializeObject<CreateUserRequest>(proxyRequest?.Body, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            var result = await _userAppService.CreateUser(request);

            return BuildResponse(result.Code, result.Data);
        }

        private APIGatewayProxyResponse BuildResponse(int statusCode, object body = null)
        {
            return new APIGatewayProxyResponse
            {
                StatusCode = statusCode,
                Body = JsonConvert.SerializeObject(body),
                Headers = new Dictionary<string, string>()
                {
                    { "Access-Control-Allow-Origin", "*" },
                    { "Access-Control-Allow-Credentials", "true" },
                    { "Cache-Control", "no-cache" }
                }
            };
        }
    }
}
