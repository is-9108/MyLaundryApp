# MyLaundryApp

AWS Lambda をクリーンアーキテクチャで構成したベースプロジェクトです。CDK でデプロイできます。

## 構成

```
MyLaundlyApp/
├── src/
│   ├── MyLaundryApp.Domain      # エンティティ・ドメイン（他層に依存しない）
│   ├── MyLaundryApp.Application # ユースケース・ポート（Domain に依存）
│   ├── MyLaundryApp.Infrastructure # アダプター実装（Application に依存）
│   └── MyLaundryApp.Presentation  # Lambda エントリポイント（Application + Infrastructure）
├── cdk/
│   └── MyLaundryApp.Cdk        # CDK スタック（Lambda デプロイ）
├── cdk.json                    # CDK エントリ設定
└── MyLaundryApp.sln
```

## 前提

- .NET 8 SDK
- AWS CLI 設定済み（または CDK_DEFAULT_ACCOUNT / CDK_DEFAULT_REGION）
- Node.js（CDK CLI 用: `npm install -g aws-cdk`）

## ビルド・デプロイ

```bash
cd C:\Users\is910\source\repos\MyLaundlyApp

# 1. Lambda を publish
dotnet publish src/MyLaundryApp.Presentation/MyLaundryApp.Presentation.csproj -c Release

# 2. CDK ブートストラップ（初回のみ）
cdk bootstrap

# 3. デプロイ
cdk deploy
```

## ローカル実行

Lambda のハンドラーは `Function.FunctionHandler` です。  
AWS Lambda .NET Mock Test Tool や、単体テストから Application 層のユースケースを呼び出す形で検証できます。
