using Amazon.CDK;
using Amazon.CDK.AWS.Lambda;
using Constructs;

namespace MyLaundryApp.Cdk;

public class MyLaundryAppStack : Stack
{
    public MyLaundryAppStack(Construct scope, string id, IStackProps? props = null)
        : base(scope, id, props)
    {
        // CDK 実行時の BaseDirectory (bin/Debug/net8.0) からリポジトリルートへ遡り、Lambda の publish フォルダを指す
        var assetPath = Path.GetFullPath(
            Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "..", "src", "MyLaundryApp.Presentation", "bin", "Release", "net8.0", "publish"));

        _ = new Function(this, "ApiFunction", new FunctionProps
        {
            Runtime = Runtime.DOTNET_8,
            Code = Code.FromAsset(assetPath),
            Handler = "MyLaundryApp.Presentation::MyLaundryApp.Presentation.Function::FunctionHandler",
            Timeout = Duration.Seconds(30)
        });
    }
}
