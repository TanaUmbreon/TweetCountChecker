using System.IO;
using Microsoft.Extensions.DependencyInjection;
using TweetCountChecker.Commands;
using TweetCountChecker.Common;
using TweetCountChecker.Infrastractures;
using TweetCountChecker.Models.Settings;
using TweetCountChecker.Repositories;

namespace TweetCountChecker
{
    /// <summary>
    /// DI コンテナに使用するサービス コレクションのスタートアップを行います。
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// <see cref="Startup"/> の新しいインスタンスを生成します。
        /// </summary>
        public Startup() { }

        /// <summary>
        /// 指定したサービス コレクションを構成します。
        /// </summary>
        /// <param name="services"></param>
        public void Configure(IServiceCollection services)
        {
            ConfigureAppSettings(services);
            ConfigureRepositories(services);
            ConfigureCommands(services);
        }

        /// <summary>
        /// 指定したサービス コレクションに対してアプリケーション設定を構成します。
        /// </summary>
        /// <param name="services"></param>
        private void ConfigureAppSettings(IServiceCollection services)
        {
            string filePath = Path.Combine(AssemblyInfo.DirectoryPath, "AppSettings.json");
            var settings = new JsonSettingsRepository(filePath);

            // ルート(アプリケーション設定リポジトリ)を登録
            services.AddSingleton<ISettingsRepository, JsonSettingsRepository>(s => settings);

            // リポジトリ内の個別オブジェクトも登録
            services.AddSingleton(s => settings.CountCommand);
            services.AddSingleton(s => settings.TwitterApi.Tokens);
            services.AddSingleton(s => settings.TwitterApi.UserTimeline);
        }

        /// <summary>
        /// 指定したサービス コレクションに対してリポジトリを構成します。
        /// </summary>
        /// <param name="services"></param>
        private void ConfigureRepositories(IServiceCollection services)
        {
            services.AddSingleton<ITwitterRepository, CoreTweetTwitterRepository>();
        }

        /// <summary>
        /// 指定したサービス コレクションに対してコマンドを構成します。
        /// </summary>
        /// <param name="services"></param>
        private void ConfigureCommands(IServiceCollection services)
        {
            services.AddTransient<CountCommand>();
        }
    }
}
