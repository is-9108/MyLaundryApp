# MyLaundryApp

**今日・明日の天気をもとに「洗濯物を外に干してよいか」を判断するアプリ**です。  
OpenWeatherMap の天気予報を取得し、雨が予想される場合は室内に干すよう、そうでない場合は外に干してよいとメッセージを返します。

- クリーンアーキテクチャ（Domain / Application / Infrastructure / Presentation）で構成
- 実行基盤は **AWS Lambda**
- インフラは **AWS CDK** で定義・デプロイ

---

## アプリの動き

1. Lambda が起動する（手動 invoke や API 経由など）
2. Application 層が Infrastructure の天気サービス（OpenWeatherMap）を呼び出す
3. 今日・明日に雨が含まれるかどうかを判定
4. 結果に応じて「洗濯物は外に干しても大丈夫です。」または「洗濯物は室内に干しましょう。」を返す

---

## AWS 構成

| リソース | 説明 |
|----------|------|
| **Lambda (ApiFunction)** | .NET 8 ランタイム。天気取得と洗濯判断のエントリポイント。環境変数 `OPENWEATHERMAP_API_KEY` で API キーを受け取る。 |
| **CDK Bootstrap スタック** | 初回のみ。デプロイ用 S3 バケットや IAM ロールなどを用意。 |

デプロイ先のアカウント・リージョンは `.env` の `CDK_DEFAULT_ACCOUNT` / `CDK_DEFAULT_REGION` で指定します。

---

## リポジトリ構成

```
MyLaundryApp/
├── src/
│   ├── MyLaundryApp.Domain       # エンティティ（例: Laundry）・ドメイン（他層に依存しない）
│   ├── MyLaundryApp.Application  # ユースケース・ポート（IWeatherService 等）
│   ├── MyLaundryApp.Infrastructure # 天気 API 呼び出し（OpenWeatherMap）などアダプター実装
│   └── MyLaundryApp.Presentation # Lambda エントリポイント（Function.FunctionHandler）
├── cdk/
│   └── MyLaundryApp.Cdk          # CDK スタック（Lambda のデプロイ定義）
├── .env.example                  # 環境変数サンプル（.env の雛形）
├── cdk.json                      # CDK エントリ設定
└── MyLaundryApp.sln
```

---

## 前提

- .NET 8 SDK
- AWS CLI 設定済み（または .env でアカウント・リージョン指定）
- Node.js（CDK CLI 用: `npm install -g aws-cdk`）
- OpenWeatherMap の API キー（[openweathermap.org/api](https://openweathermap.org/api) で取得）

---

## ビルド・デプロイ（CDK）

リポジトリルートで以下を実行します。

1. **`.env` の準備**  
   `.env.example` をコピーして `.env` を作成し、次を設定する。
   - `CDK_DEFAULT_ACCOUNT` … AWS アカウント ID（12 桁）
   - `CDK_DEFAULT_REGION` … デプロイ先リージョン（例: `ap-northeast-1`）
   - `OPENWEATHERMAP_API_KEY` … OpenWeatherMap の API キー（Lambda に渡される）

2. **Lambda を publish**

   ```bash
   dotnet publish src/MyLaundryApp.Presentation/MyLaundryApp.Presentation.csproj -c Release
   ```

3. **CDK ブートストラップ（初回のみ）**

   ```bash
   cdk bootstrap
   ```

4. **デプロイ**

   ```bash
   cdk deploy
   ```

CDK はリポジトリルートの `.env` を読み込み、アカウント・リージョンと API キーを利用します。Lambda には `OPENWEATHERMAP_API_KEY` が環境変数として渡されます。

---

## ローカル・検証

- Lambda のハンドラーは `MyLaundryApp.Presentation.Function::FunctionHandler` です。
- AWS Lambda .NET Mock Test Tool や、単体テストから Application 層のユースケースを呼び出す形で検証できます。
