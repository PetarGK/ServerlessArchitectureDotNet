using Amazon.CDK;
using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.ECS;
using Amazon.CDK.AWS.ElasticLoadBalancingV2;
using Amazon.CDK.AWS.ECR;
using Amazon.JSII.Runtime.Deputy;
using HealthCheck = Amazon.CDK.AWS.ElasticLoadBalancingV2.HealthCheck;
using Amazon.CDK.AWS.IAM;
using System.Collections.Generic;
using Amazon.CDK.AWS.ApplicationAutoScaling;
using Amazon.CDK.AWS.ECS.Patterns;

namespace Minniowa.Cloud.Stacks
{
    public interface IUsersServiceFargateStackProps : IStackProps
    {
        IVpc Vpc { get; set; }
        ICluster Cluster { get; set; }
        ApplicationListener Listener { get; set; }
    }

    public class UsersServiceFargateStackProps : DeputyBase , IUsersServiceFargateStackProps
    {
        public virtual IVpc Vpc { get; set; }
        public virtual ICluster Cluster { get; set; }
        public virtual ApplicationListener Listener { get; set; }
    }

    public class UsersServiceFargateStack : Stack
    {
        internal UsersServiceFargateStack(Construct scope, string id, IUsersServiceFargateStackProps props) : base(scope, id, props)
        {
            var repository = Repository.FromRepositoryName(this, "UsersRepository", "users-service");

            
            var taskDefinition = new FargateTaskDefinition(this, "UsersServiceTaskDef", new FargateTaskDefinitionProps
            {
                Cpu = 256,
                MemoryLimitMiB = 512
            });

            var container = taskDefinition.AddContainer("WebApi", new ContainerDefinitionOptions
            {
                Image = ContainerImage.FromEcrRepository(repository),
                Logging = new AwsLogDriver(new AwsLogDriverProps
                {
                    LogRetention = Amazon.CDK.AWS.Logs.RetentionDays.TWO_WEEKS,
                    StreamPrefix = "UsersService-"
                })
            });

            container.AddPortMappings(new PortMapping
            {
                ContainerPort = 80
            });

            var service = new FargateService(this, "UsersService", new FargateServiceProps
            {
                Cluster = props.Cluster,
                TaskDefinition = taskDefinition,
                DesiredCount = 2,
                VpcSubnets = new SubnetSelection
                {
                    SubnetType = SubnetType.PUBLIC
                },
                AssignPublicIp = true
            });

            service.AutoScaleTaskCount(new EnableScalingProps
            {
                MinCapacity = 2,
                MaxCapacity = 4
            }).ScaleOnCpuUtilization("CPU", new CpuUtilizationScalingProps
            {
                TargetUtilizationPercent = 70
            });

            props.Listener.AddTargets("UsersServiceTarget", new AddApplicationTargetsProps
            {
                HealthCheck = new HealthCheck
                {
                    HealthyHttpCodes = "200-299",
                    Path = "/api/users/health",
                    Protocol = Amazon.CDK.AWS.ElasticLoadBalancingV2.Protocol.HTTP,
                    HealthyThresholdCount = 2,
                    UnhealthyThresholdCount = 5,
                    Timeout = Duration.Seconds(10),
                    Interval = Duration.Seconds(70),
                    Port = "80"
                },
                Protocol = ApplicationProtocol.HTTP,
                Port = 80,
                Targets = new IApplicationLoadBalancerTarget[] { service },
                PathPattern = "/api/users*",
                Priority = 2
            });
            
        }
    }
}
