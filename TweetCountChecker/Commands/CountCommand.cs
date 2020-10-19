using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using TweetCountChecker.Models.Settings;
using TweetCountChecker.Models.Twitter;
using TweetCountChecker.Repositories;

namespace TweetCountChecker.Commands
{
    /// <summary>
    /// 特定キーワードのツイート回数を集計します。
    /// </summary>
    public class CountCommand : ICommand
    {
        private readonly ITwitterRepository twitter;
        private readonly CountCommandSettings settings;

        /// <summary>
        /// <see cref="CountCommand"/> の新しいインスタンスを生成します。
        /// </summary>
        public CountCommand(ITwitterRepository twitter, CountCommandSettings settings)
        {
            this.twitter = twitter;
            this.settings = settings;
        }

        public void Execute()
        {
            Console.WriteLine($"[Info] ユーザーのタイムラインのツイートを取得しています。");

            // Memo: API経由で@IDからユーザーIDを取得したかったけど手抜きによりハードコード
            long userId = 1257075494L; // @UmbreonTanaのユーザーID

            IEnumerable<Tweet> tweets = GetUserTimelineTweetsSafety(userId);
            if (!tweets.Any()) { throw new ApplicationException("タイムラインのツイートが一つも取得できませんでした。処理を中断します。"); }

            Console.WriteLine($"[Info] ツイートを集計しています。");
            var report = new Report(userId, settings.SearchWord, tweets);

            Console.WriteLine($"[Info] 集計結果を出力しています。");
            ExportText(report);
        }

        /// <summary>
        /// 指定したユーザー ID のタイムラインのツイートを取得します。
        /// 取得中に例外が発生した場合は、例外をスローせずにツイート取得を中断し、
        /// その時点で取得できたツイートを返します。
        /// </summary>
        /// <returns></returns>
        private IEnumerable<Tweet> GetUserTimelineTweetsSafety(long userId)
        {
            var tweets = new List<Tweet>();

            try
            {
                foreach (Tweet t in twitter.GetUserTimelineTweets(userId))
                {
                    tweets.Add(t);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Warn] タイムラインのツイートを取得中に問題が発生しました。\nツイート取得を中断し、問題が発生するまでに取得できたツイートで処理を続行します。\n\n[例外メッセージ]\n{ex.Message}\n\n[スタック トレース]\n{ex.StackTrace}");
            }

            return tweets;
        }

        private class Report
        {
            public long UserId { get; }
            public int TotalCount { get; }
            public DateTime BeginDate { get; }
            public DateTime EndDate { get; }
            public double TotalDays { get; }
            public IEnumerable<Tweet> MatchedTweets { get; }
            public int MatchedCount { get; }
            public double CountPerDay { get; }
            public double OnceEveryTime { get; }

            public Report(long userId, string searchWord, IEnumerable<Tweet> tweets)
            {
                UserId = userId;

                TotalCount = tweets.Count();
                BeginDate = tweets.Min(t => t.TweetedAt);
                EndDate = tweets.Max(t => t.TweetedAt);
                TotalDays = (EndDate - BeginDate).TotalDays;

                var regex = new Regex(searchWord);
                MatchedTweets = tweets.Where(t => regex.IsMatch(t.Text));
                MatchedCount = MatchedTweets.Count();
                CountPerDay = TotalDays == 0.0 ? 0 : MatchedCount / TotalDays;
                OnceEveryTime = MatchedCount == 0.0 ? 0 : TotalCount / MatchedCount;
            }
        }

        private void ExportText(Report report)
        {

            string path = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                "ツイート回数集計結果.md");
            using var writer = new StreamWriter(path: path, append: false, encoding: Encoding.UTF8);

            writer.WriteLine("# ツイート回数集計結果");
            writer.WriteLine();
            writer.WriteLine("## 対象ユーザー");
            writer.WriteLine();
            writer.WriteLine($"@{settings.TargetUserAtId} (ID: {report.UserId})");
            writer.WriteLine();
            writer.WriteLine("## 対象ワード (正規表現)");
            writer.WriteLine();
            writer.WriteLine($"`{settings.SearchWord}`");
            writer.WriteLine();
            writer.WriteLine("## 対象ワードのツイート頻度");
            writer.WriteLine();
            writer.WriteLine($"{report.CountPerDay:0.0} 回/日");
            writer.WriteLine();
            writer.WriteLine($"{report.OnceEveryTime:0.0} 回に1回");
            writer.WriteLine();
            writer.WriteLine("## 集計期間");
            writer.WriteLine();
            writer.WriteLine($"{report.BeginDate:yyyy/M/d H:mm:ss} ～ {report.EndDate:yyyy/M/d H:mm:ss} ({report.TotalDays:0.0} 日)");
            writer.WriteLine();
            writer.WriteLine("## 集計総ツイート数");
            writer.WriteLine();
            writer.WriteLine($"{report.TotalCount} (※ただし、通常RTと他ユーザーへのリプライを除く)");
            writer.WriteLine();
            writer.WriteLine("## ツイート回数");
            writer.WriteLine();
            writer.WriteLine($"{report.MatchedCount} 回");
            writer.WriteLine();
            writer.WriteLine("----");
            writer.WriteLine();
            writer.WriteLine("## ツイート明細");
            writer.WriteLine();
            foreach (Tweet t in report.MatchedTweets)
            {
                writer.WriteLine($"1. **{t.Text}** ({t.TweetedAt:yyyy/M/d H:mm:ss})");
            }
        }
    }
}
