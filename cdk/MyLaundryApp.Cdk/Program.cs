using Amazon.CDK;
using MyLaundryApp.Cdk;

var app = new App();

_ = new MyLaundryAppStack(app, "MyLaundryAppStack", new StackProps
{
    Env = new Amazon.CDK.Environment
    {
        Account = System.Environment.GetEnvironmentVariable("CDK_DEFAULT_ACCOUNT"),
        Region = System.Environment.GetEnvironmentVariable("CDK_DEFAULT_REGION")
    }
});

app.Synth();
