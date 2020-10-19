namespace TweetCountChecker.Models.Settings
{
    /// <summary>
    /// ツイート数カウントの設定を表します。
    /// </summary>
    public class CountCommandSettings
    {
        /// <summary>キー名</summary>
        public const string KeyName = "CountCommand";

        /// <summary>
        /// カウント対象となるユーザー アカウントの @ID を取得または設定します。
        /// </summary>
        public string TargetUserAtId { get; set; } = "";

        /// <summary>
        /// カウント対象のツイートを抽出する検索ワードを取得または設定します。
        /// </summary>
        /// <remarks>部分一致で検索を行い、全角半角や大文字小文字などは区別します。正規表現での記述に対応しています。</remarks>
        public string SearchWord { get; set; } = "";

        /// <summary>
        /// <see cref="CountCommandSettings"/> の新しいインスタンスを生成します。
        /// </summary>
        public CountCommandSettings() { }
    }
}
