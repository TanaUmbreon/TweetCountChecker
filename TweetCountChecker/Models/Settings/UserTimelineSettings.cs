using System;

namespace TweetCountChecker.Models.Settings
{
    /// <summary>
    /// Twitter API のユーザー タイムライン問い合わせで使用する設定を表します。
    /// </summary>
    public class UserTimelineSettings
    {
        /// <summary>キー名</summary>
        public const string KeyName = "UserTimeline";

        private const int DefaultCountPerQuery = 200;
        private int countPerQuery = DefaultCountPerQuery;

        private const int DefaultMaxQueryIterationCount = 1;
        private int maxQueryIterationCount = DefaultMaxQueryIterationCount;

        /// <summary>
        /// 一度のタイムライン問い合わせで取得する最大ツイート数を取得または設定します。
        /// </summary>
        public int CountPerQuery
        {
            get => countPerQuery;
            set
            {
                if (value < 1 || value > 200)
                {
                    throw new ArgumentOutOfRangeException(
                        $"'{KeyName}' の '{nameof(CountPerQuery)}' は 1 ～ 200 の範囲で設定してください。規定値は {DefaultCountPerQuery} です。");
                }
                countPerQuery = value;
            }
        }
        /// <summary>
        /// タイムライン問い合わせを繰り返す回数を取得または設定します。
        /// </summary>
        /// <remarks>Twitter APIの仕様上、タイムライン問い合わせは15分あたり15回までです。</remarks>
        public int MaxQueryIterationCount
        {
            get => maxQueryIterationCount;
            set
            {
                if (value < 1 || value > 15)
                {
                    throw new ArgumentOutOfRangeException(
                        $"'{KeyName}' の '{nameof(MaxQueryIterationCount)}' は 1 ～ 15 の範囲で設定してください。規定値は {DefaultMaxQueryIterationCount} です。");
                }
                maxQueryIterationCount = value;
            }
        }

        /// <summary>
        /// <see cref="UserTimelineSettings"/> の新しいインスタンスを生成します。
        /// </summary>
        public UserTimelineSettings() { }
    }
}
