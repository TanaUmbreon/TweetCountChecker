namespace TweetCountChecker.Models.Settings
{
    /// <summary>
    /// Twitter API の設定を表します。
    /// </summary>
    public class TwitterApiSettings
    {
        /// <summary>キー名</summary>
        public const string KeyName = "TwitterApi";

        /// <summary>
        /// Twitter Developers で作成した App から発行される Consumer API keys および Access tokens の設定を取得または設定します。
        /// </summary>
        public TokensSettings? Tokens { get; set; } = null;

        /// <summary>
        /// Twitter API のユーザー タイムライン問い合わせで使用する設定を取得または設定します。
        /// </summary>
        public UserTimelineSettings? UserTimeline { get; set; } = null;

        /// <summary>
        /// <see cref="TwitterApiSettings"/> の新しいインスタンスを生成します。
        /// </summary>
        public TwitterApiSettings() { }
    }
}
