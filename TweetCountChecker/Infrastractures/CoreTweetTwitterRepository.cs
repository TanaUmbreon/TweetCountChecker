using System;
using System.Collections.Generic;
using CoreTweet;
using TweetCountChecker.Models.Settings;
using TweetCountChecker.Models.Twitter;
using TweetCountChecker.Repositories;

namespace TweetCountChecker.Infrastractures
{
    /// <summary>
    /// CoreTweet による Twitter 操作を提供します。
    /// </summary>
    public class CoreTweetTwitterRepository : ITwitterRepository
    {
        private readonly TokensSettings tokensSettings;
        private readonly UserTimelineSettings userTimelineSettings;

        /// <summary>
        /// <see cref="CoreTweetTwitterRepository"/> の新しいインスタンスを生成します。
        /// </summary>
        public CoreTweetTwitterRepository(
            TokensSettings tokensSettings,
            UserTimelineSettings userTimelineSettings)
        {
            this.tokensSettings = tokensSettings;
            this.userTimelineSettings = userTimelineSettings;
        }

        /// <summary>
        /// 指定したユーザー ID のタイムラインからツイートのコレクションを取得します。
        /// </summary>
        /// <param name="userId">タイムラインを取得するユーザー ID。</param>
        /// <returns>指定したユーザーのタイムラインから取得したツイートのコレクション。</returns>
        public IEnumerable<Tweet> GetUserTimelineTweets(long userId)
        {
            Tokens t = CreateTokens();
            long? maxId = null;

            for (int i = 0; i < userTimelineSettings.MaxQueryIterationCount; ++i)
            {
                var statuses = t.Statuses.UserTimeline(
                    user_id: userId,
                    count: userTimelineSettings.CountPerQuery,
                    max_id: maxId,
                    include_rts: false);

                foreach (Status status in statuses)
                {
                    // 他ユーザーへのリプライは取得しない (自分へのリプライは取得する)
                    if (status.InReplyToUserId != null && status.InReplyToUserId != userId) { continue; }

                    yield return new Tweet()
                    {
                        TweetId = status.Id,
                        UserId = status.User.Id ?? 0L,
                        UserScreenName = status.User.ScreenName ?? "",
                        UserName = status.User.Name ?? "",
                        TweetedAt = ToDateTime(status.Id),
                        Text = status.Text.Replace("\n", "\\n"),
                    };

                    maxId = status.Id;
                }
            }
        }

        /// <summary>
        /// Twitter API トークンを生成します。
        /// </summary>
        /// <returns></returns>
        private Tokens CreateTokens() => Tokens.Create(
            tokensSettings.ApiKey,
            tokensSettings.ApiSecretKey,
            tokensSettings.AccessToken,
            tokensSettings.AccessTokenSecret);

        /// <summary>
        /// 指定したツイート ID からミリ秒単位まで含まれるツイート日時に変換して返します。
        /// </summary>
        /// <param name="tweetId"></param>
        /// <returns></returns>
        private DateTime ToDateTime(long tweetId)
        {
            // 算出方法の参考元
            // https://yoshipc.net/tweet-id-to-mili-sec/

            const long SnowflakeBeginTimestamp = 1288834974657L;
            long timestamp = (tweetId >> 22) + SnowflakeBeginTimestamp;
            if (timestamp < 0L) { return DateTime.MinValue; }
            return DateTimeOffset.FromUnixTimeMilliseconds(timestamp).LocalDateTime;
        }
    }
}
