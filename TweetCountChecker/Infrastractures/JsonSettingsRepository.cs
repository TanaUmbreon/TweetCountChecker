using Microsoft.Extensions.Configuration;
using TweetCountChecker.Models.Settings;
using TweetCountChecker.Repositories;

namespace TweetCountChecker.Infrastractures
{
    /// <summary>
    /// JSON 形式のアプリケーション設定ファイルを操作します。
    /// </summary>
    public class JsonSettingsRepository : ISettingsRepository
    {
        /// <summary>
        /// Twitter API の設定を取得します。
        /// </summary>
        public TwitterApiSettings TwitterApi { get; }

        /// <summary>
        /// ツイート数カウントの設定を取得します。
        /// </summary>
        public CountCommandSettings CountCommand { get; }

        /// <summary>
        /// 指定したファイル パスから <see cref="JsonSettingsRepository"/> の新しいインスタンスを生成します。
        /// </summary>
        /// <param name="filePath"></param>
        public JsonSettingsRepository(string filePath)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile(filePath)
#if DEBUG
                .AddUserSecrets<Startup>() // UserSecretsにデバッグ用の機密データを設定する為指定
#endif
                .Build();

            TwitterApi = config.GetSection(TwitterApiSettings.KeyName).Get<TwitterApiSettings>();
            CountCommand = config.GetSection(CountCommandSettings.KeyName).Get<CountCommandSettings>();
        }

    }
}
