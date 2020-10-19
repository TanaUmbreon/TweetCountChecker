using System;

namespace TweetCountChecker.Models.Twitter
{
    /// <summary>
    /// 単一のツイートを格納します。
    /// </summary>
    public class Tweet
    {
        /// <summary>
        /// ツイート ID を取得または設定します。
        /// </summary>
        public long TweetId { get; set; } = 0L;

        /// <summary>
        /// ユーザー ID を取得または設定します。
        /// </summary>
        public long UserId { get; set; } = 0L;

        /// <summary>
        /// ユーザー スクリーン (@ID) 名を取得または設定します。
        /// </summary>
        public string UserScreenName { get; set; } = "";

        /// <summary>
        /// ユーザー名を取得または設定します。
        /// </summary>
        public string UserName { get; set; } = "";

        /// <summary>
        /// ツイート日時を取得または設定します。
        /// </summary>
        public DateTime TweetedAt { get; set; } = DateTime.MinValue;

        /// <summary>
        /// ツイート本文を取得または設定します。
        /// </summary>
        public string Text { get; set; } = "";

        /// <summary>
        /// <see cref="Tweet"/> の新しいインスタンスを生成します。
        /// </summary>
        public Tweet() { }
    }
}
