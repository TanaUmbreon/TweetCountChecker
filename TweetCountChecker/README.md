# 開発者向けREADME

## コンフィグファイル (AppSettings.json) について

UserSecretsを利用し、ローカル開発環境で使用するが、バージョン管理に含めるべきでない機密情報 (Twitter APIのシークレットキー情報等) を分離するようにしています。

このUserSecretsの利用はプロジェクト構成がDebug構成時のみに適用され、Release構成時には適用されません。 (`Startup.cs` にて制御)

### UserSecretsとは

UserSecretsを利用することで、コンフィグファイルを公開用 (リポジトリ管理対象) と非公開用 (ローカル開発環境) に使い分けることができるようになります。この非公開用に使われるファイルがUserSecretsです。主にASP.NET Coreで使われています。

UserSecretsがある場合、本来利用されるコンフィグファイルの代わりに利用されます。UserSecretsがない場合は本来利用されるコンフィグファイルが利用されます。

UserSecretsはプロジェクトツリーから隔離された場所で管理します。(`%APPDATA%\Microsoft\UserSecrets\<UserSecretsId>\secrets.json`)

`<UserSecretsId>` は、プロジェクトファイル (*.csproj) の `<PropertyGroup>.<UserSecretsId>` に定義したGUIDが使用されます。

UserSecretsファイルは平文のテキストファイルのため、あくまで開発目的のみで使用されます。

ASP.NET CoreではUserSecretsを容易に管理するためのツールがVisual Studioのメニュー拡張として自動で追加されますが、他の開発プロジェクトでは追加されません。そのため、ここでは同様の機能を持った拡張機能「Open UserSecrets」を使用しています。

## 設計方針

オニオンアーキテクチャの概念を採用しています。

また、一部オブジェクトの生成にはコンストラクタによるDI (依存性の注入) を採用しています。

### オニオンアーキテクチャの階層定義

↑内層

- Models (Domain Model)
  - Repositories (Domain Service)
    - Commands (Application Service)
      - Infrastractures (Infrastracure)

↓外層

上位の段落が内層、下位の段落が外層という関係性を表します。カッコ内はオニオンアーキテクチャの定義で用いられる名称で、本アプリで用いる用語との対比として記載しています。

外層から内層方向に向かう依存関係のみ許可されます。

階層名は、ソースコード上の名前空間と一致します。
