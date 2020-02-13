using Amazon.CDK;
using Amazon.CDK.AWS.APIGateway;
using Amazon.CDK.AWS.Lambda;

namespace Minniowa.Cloud.Stacks
{
    public class UsersServiceLambdaStack : Stack
    {
        internal UsersServiceLambdaStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            var lambda = new Function(this, "CreateUser", new FunctionProps
            {
                Runtime = Runtime.DOTNET_CORE_2_1,
                Code = Code.FromAsset("../Minniowa.Users.Lambda/bin/Debug/netcoreapp2.1/publish"),
                Handler = "Minniowa.Users.Lambda::Minniowa.Users.Lambda.Functions::CreateUser",
                MemorySize = 1024,
                Timeout = Duration.Seconds(30)
            });


            var api = new RestApi(this, "MinniowaApi", new RestApiProps
            {
                RestApiName = "Minniowa Service"
            });

            var v1 = api.Root.AddResource("v1");

            var getRequestLambdaIntegration = new LambdaIntegration(lambda);
            var getRequestMethod = v1.AddMethod("POST", getRequestLambdaIntegration);
        }
    }
}
