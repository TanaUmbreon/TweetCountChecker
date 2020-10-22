using System.Collections.Generic;
using TweetCountChecker.Models.Twitter;

namespace TweetCountChecker.Repositories
{
    /// <summary>
    /// Twitter API の直接的な操作を隠蔽し、抽象度の高いアプリケーション寄りな操作を提供します。
    /// </summary>
    public interface ITwitterRepository
    {
        /// <summary>
        /// 指定したユーザー スクリーン名 (@ID) からユーザー IDを取得します。
        /// </summary>
        /// <param name="userScreenName">ユーザー スクリーン名 (@ID)。</param>
        /// <returns>ユーザー スクリーン名 (@ID) に一致するユーザー ID。</returns>
        public long GetUserId(string userScreenName);

        /// <summary>
        /// 指定したユーザー ID のタイムラインからツイートのコレクションを取得します。
        /// </summary>
        /// <param name="userId">タイムラインを取得するユーザー ID。</param>
        /// <returns>指定したユーザーのタイムラインから取得したツイートのコレクション。</returns>
        IEnumerable<Tweet> GetUserTimelineTweets(long userId);
    }
}
