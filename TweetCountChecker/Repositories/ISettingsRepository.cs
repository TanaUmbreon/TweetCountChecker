using TweetCountChecker.Models.Settings;

namespace TweetCountChecker.Repositories
{
    /// <summary>
    /// アプリケーション設定ファイルの直接的な操作を隠蔽し、抽象度の高いアプリケーション寄りな操作を提供します。
    /// </summary>
    public interface ISettingsRepository
    {
        /// <summary>
        /// Twitter API の設定を取得します。
        /// </summary>
        TwitterApiSettings TwitterApi { get; }

        /// <summary>
        /// ツイート数カウントの設定を取得します。
        /// </summary>
        CountCommandSettings CountCommand { get; }
    }
}
