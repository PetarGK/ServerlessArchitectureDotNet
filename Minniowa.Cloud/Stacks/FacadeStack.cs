using Amazon.CDK;
using Amazon.CDK.AWS.AppSync;
using System;
using System.Collections.Generic;
using System.Text;

namespace Minniowa.Cloud.Stacks
{
    public class FacadeStack : Stack
    {
        internal FacadeStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            var api = new GraphQLApi(this, "Api", new GraphQLApiProps
            {
                Name = "DemoApi",
                LogConfig = new LogConfig
                {
                    FieldLogLevel = FieldLogLevel.ALL
                },
                SchemaDefinitionFile = "./schema.graphql"
            });


        }
    }
}
