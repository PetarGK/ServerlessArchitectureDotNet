using Amazon.CDK;
using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.ElasticLoadBalancingV2;
using Amazon.JSII.Runtime.Deputy;

namespace Minniowa.Cloud.Stacks
{
    public interface IInternalLoadBalancerStackProps : IStackProps
    {
        IVpc Vpc { get; set; }
    }

    public class InternalLoadBalancerStackProps : DeputyBase , IInternalLoadBalancerStackProps
    {
        public virtual IVpc Vpc { get; set; }
    }

    public class InternalLoadBalancerStack : Stack
    {
        public ApplicationListener Listener { get; private set; }

        internal InternalLoadBalancerStack(Construct scope, string id, IInternalLoadBalancerStackProps props) : base(scope, id, props)
        {
            var loadBalancer = new ApplicationLoadBalancer(this, "LoadBalancer", new ApplicationLoadBalancerProps
            {
                Vpc = props.Vpc, 
                InternetFacing = true
            });

            var defaultTarget = new ApplicationTargetGroup(this, "TargetGroup", new ApplicationTargetGroupProps
            {
                Vpc = props.Vpc,
                Port = 80
            });

            Listener = loadBalancer.AddListener("Listener", new BaseApplicationListenerProps
            {
                Port = 80,
                DefaultTargetGroups = new ApplicationTargetGroup[] { defaultTarget }
            });

            new CfnOutput(this, "LoadBalancerDNS", new CfnOutputProps
            {
                Value = loadBalancer.LoadBalancerDnsName
            });
        }
    }
}
