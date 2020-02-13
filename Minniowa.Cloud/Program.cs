using Amazon.CDK;
using Minniowa.Cloud.Stacks;

namespace Minniowa.Cloud
{
    sealed class Program
    {
        static void Main(string[] args)
        {
            var app = new App();

            var common = new CommonStack(app, "CommonStack");
            
            var loadBalancer = new InternalLoadBalancerStack(app, "InternalLoadBalancerStack", new InternalLoadBalancerStackProps
            {
                Vpc = common.Vpc
            });
            
            //var usersService = new UsersServiceLambdaStack(app, "UsersServiceStack");

            var usersServiceFargate = new UsersServiceFargateStack(app, "UsersServiceFargateStack", new UsersServiceFargateStackProps
            {
                Vpc = common.Vpc,
                Cluster = common.Cluster,
                Listener = loadBalancer.Listener
            });
            
            //var facade = new FacadeStack(app, "FacadeStack");

            app.Synth();
        }
    }
}
