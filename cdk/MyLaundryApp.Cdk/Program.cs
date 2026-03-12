using Amazon.CDK;
using DotNetEnv;
using MyLaundryApp.Cdk;

// リポジトリルートの .env を読み込み（存在する場合）
var repoRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", ".."));
var envPath = Path.Combine(repoRoot, ".env");
if (File.Exists(envPath))
    Env.Load(envPath);

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
