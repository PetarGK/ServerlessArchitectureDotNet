using Amazon.CDK;
using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.ECS;

namespace Minniowa.Cloud.Stacks
{
    public class CommonStack : Stack
    {
        public Vpc Vpc { get; private set; }
        public Cluster Cluster { get; private set; }

        internal CommonStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            Vpc = new Vpc(this, "Vpc", new VpcProps
            {
                Cidr = "10.0.0.0/21",
                MaxAzs = 3,
                NatGateways = 0,
                SubnetConfiguration = new SubnetConfiguration[]
                {
                    new SubnetConfiguration
                    {
                        SubnetType = SubnetType.PUBLIC,
                        Name = "Public"
                    }
                }
            });
            /*
            new InterfaceVpcEndpoint(this, "ECREndpoint", new InterfaceVpcEndpointProps
            {
                Vpc = Vpc,
                Service = InterfaceVpcEndpointAwsService.ECR_DOCKER
            });

            new InterfaceVpcEndpoint(this, "LogsEndpoint", new InterfaceVpcEndpointProps
            {
                Vpc = Vpc,
                Service = InterfaceVpcEndpointAwsService.CLOUDWATCH_LOGS
            });
            */
            Cluster = new Cluster(this, "Cluster", new ClusterProps
            {
                Vpc = Vpc
            });
        }
    }
}
